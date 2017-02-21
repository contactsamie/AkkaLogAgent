using AkkaLogAgent.Common;
using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AkkaLogAgent.Services
{
    public class LogWatchServiceAgentService
    {
        private ILogFileUpdateHandler LogFileUpdateHandler { set; get; }

        public LogWatchServiceAgentService(ILogFileUpdateHandler logFileUpdateHandler)
        {
            LogFileUpdateHandler = logFileUpdateHandler;
        }

        private readonly FileSystemWatcher _watcher = new FileSystemWatcher();
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private List<IAgentLogConsumer> Consumers { set; get; }

        public void StopWatchingFiles()
        {
            LogFileUpdateHandler.Reset();

            _watcher.EnableRaisingEvents = false;
            foreach (var agentLogConsumer in Consumers)
            {
                agentLogConsumer.OnStoped();
            }
        }

        public void StartWatchingFiles(bool debugMode, string path, string fileFilter, params IAgentLogConsumer[] consumers)
        {
            LogFileUpdateHandler.Reset();

            Consumers = consumers.ToList();
            _watcher.Path = path;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Filter = fileFilter;
            _watcher.Changed += OnChanged;
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
            foreach (var agentLogConsumer in Consumers)
            {
                agentLogConsumer.Start(debugMode);
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var path = e.FullPath;

            LogFileUpdateHandler.HandleFileChangeEvent(Consumers, path);
        }
    }
}