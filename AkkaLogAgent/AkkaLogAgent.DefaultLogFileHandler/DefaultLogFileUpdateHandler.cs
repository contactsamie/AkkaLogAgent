using AkkaLogAgent.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaLogAgent.DefaultLogFileHandler
{
    public class DefaultLogFileUpdateHandler : ILogFileUpdateHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private Dictionary<string, int> ReadLinesCount { set; get; }

        public DefaultLogFileUpdateHandler()
        {
            Reset();
        }

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

        public void Reset()
        {
            IsFirstTime = new Dictionary<string, bool>();
            ReadLinesCount = new Dictionary<string, int>();
        }

        public void HandleFileChangeEvent(List<IAgentLogConsumer> consumers, string path, int currentRetryCount = 0)
        {
            if (currentRetryCount > 3)
            {
                Log.Debug("Unable to access file after few trials : " + path);
            }
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
                ReadLinesCount[path] = totalLines;
                if (!IsFirstTime[path] && read.Count != 0)
                {
                    PushNotification(consumers, read);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Error occured while handling file change event. I will try again : " + path);
                currentRetryCount++;
                System.Threading.Thread.Sleep(1000);
                HandleFileChangeEvent(consumers, path, currentRetryCount);
            }
        }

        public void InitializeFileTrackingObjects(string path)
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

        private static void PushNotification(List<IAgentLogConsumer> consumers, List<string> message)
        {
            message.Reverse();
            var data = string.Join(Environment.NewLine, message);
            consumers = consumers ?? new List<IAgentLogConsumer>();
            message.Reverse();
            foreach (var agentLogConsumer in consumers)
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