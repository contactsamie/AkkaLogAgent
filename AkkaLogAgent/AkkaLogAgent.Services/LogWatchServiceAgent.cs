using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AkkaLogAgent.Services
{
    public class LogWatchServiceAgent
    {
        private readonly FileSystemWatcher _watcher = new FileSystemWatcher();

        private List<IAgentLogConsumer> Consumers { set; get; }

        public void StopWatchingFiles()
        {
            _watcher.EnableRaisingEvents = false;
            foreach (var agentLogConsumer in Consumers)
            {
                agentLogConsumer.OnStoped();
            }
        }

        public void StartWatchingFiles(string path, string fileFilter, params IAgentLogConsumer[] consumers)
        {
            Consumers = consumers.ToList();
            _watcher.Path = path;
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.Filter = fileFilter;
            _watcher.Changed += OnChanged;
            _watcher.Created += OnChanged;
            _watcher.Deleted += OnChanged;
            _watcher.Renamed += OnChanged;
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
            try
            {
                ReadLinesCount = ReadLinesCount ?? new Dictionary<string, int>();
                var path = e.FullPath;
                if (!ReadLinesCount.ContainsKey(path))
                {
                    ReadLinesCount.Add(path, 0);
                }

                var isFirstTime = ReadLinesCount[path] == 0;
                var totalLines = File.ReadLines(path).Count();

                if (ReadLinesCount[path] >= totalLines)
                {
                    ReadLinesCount[path] = 0;
                }
                var newLinesCount = totalLines - ReadLinesCount[path];

                var read = ReadLines(e.FullPath).Skip(ReadLinesCount[path]).Take(newLinesCount).ToList();
                ReadLinesCount[path] = totalLines < 10 ? totalLines : totalLines - 1000;
                if (!isFirstTime)
                {
                    PushNotification(read);
                }
            }
            catch (Exception)
            {
            }
        }

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