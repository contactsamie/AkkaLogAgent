using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AkkaLogAgent.Services
{
    public class LogWatchServiceAgent
    {
       
        private readonly FileSystemWatcher _watcher = new FileSystemWatcher();
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private List<IAgentLogConsumer> Consumers { set; get; }

        public void StopWatchingFiles()
        {
            IsFirstTime = new Dictionary<string, bool>();
            ReadLinesCount = new Dictionary<string, int>();
            _watcher.EnableRaisingEvents = false;
            foreach (var agentLogConsumer in Consumers)
            {
              
                agentLogConsumer.OnStoped();
            }
        }

        public void StartWatchingFiles(string path, string fileFilter, params IAgentLogConsumer[] consumers)
        {
            IsFirstTime=new Dictionary<string, bool>();
            ReadLinesCount = new Dictionary<string, int>();
            //WatchTimeOutExeeded = true;
            Consumers = consumers.ToList();
            _watcher.Path = path;
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.Filter = fileFilter;
            _watcher.Changed += OnChanged;
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            foreach (var agentLogConsumer in Consumers)
            {
                agentLogConsumer.OnStarted();
            }
        }

        private Dictionary<string, int> ReadLinesCount { set; get; }

        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var path = e.FullPath;
            //if (!WatchTimeOutExeeded) return;
            //WatchTimeOutExeeded = false;
            //Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(x =>
            //{
            //    WatchTimeOutExeeded = true;
            //});
            HandleFileChangeEvent(path);
        }

       // public bool WatchTimeOutExeeded { get; set; }

        private void HandleFileChangeEvent( string path)
        {
            try
            {
                InitializeFileTrackingObjects(path);

                IsFirstTime[path] = ReadLinesCount[path] == 0;
                var totalLines = File.ReadLines(path).Count();

                if (ReadLinesCount[path] >= totalLines)
                {
                    ReadLinesCount[path] = totalLines;
                }
                var newLinesCount = totalLines - ReadLinesCount[path];

                var read = ReadLines(path).Skip(ReadLinesCount[path]).Take(newLinesCount).ToList();
                ReadLinesCount[path] = totalLines ;
                if (!IsFirstTime[path] && read.Count!=0)
                {
                    PushNotification(read);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Error occured while handling file change event : " + path);
            }
        }

        private void InitializeFileTrackingObjects(string path)
        {
            if (!ReadLinesCount.ContainsKey(path))
            {
                ReadLinesCount.Add(path, 0);
            }
            if (!IsFirstTime.ContainsKey(path))
            {
                IsFirstTime.Add(path, true);
            }
        }

        public Dictionary<string, bool> IsFirstTime { get; set; }

        private void PushNotification(List<string> message)
        {
            message.Reverse();
            var data = string.Join(Environment.NewLine, message);
            Consumers = Consumers ?? new List<IAgentLogConsumer>();
            message.Reverse();
            foreach (var agentLogConsumer in Consumers)
            {
                agentLogConsumer?.OnBatchLogUpdate(data);
                message.ForEach(x =>
                    {
                        if (string.IsNullOrEmpty(x)) return;
                        agentLogConsumer?.OnEachLogUpdate(x);
                    }
                );
            }
        }
    }
}