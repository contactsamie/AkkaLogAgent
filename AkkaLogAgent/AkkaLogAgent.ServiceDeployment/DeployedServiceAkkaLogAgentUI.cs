using AkkaLogAgent.Common;
using AkkaLogAgent.Services;
using NLog;

namespace AkkaLogAgent.ServiceDeployment
{
    public class DeployedServiceAkkaLogAgentUI : IAkkaLogAgentUI
    {
        private LogWatchServiceAgentService ServiceAgent { set; get; }
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public DeployedServiceAkkaLogAgentUI(LogWatchServiceAgentService serviceAgent)
        {
            ServiceAgent = serviceAgent;
        }

        public void StartMonitoring(bool debugMode, string path, string fileFilter, IAgentLogConsumer agentLogConsumer)
        {
            Log.Debug("Starting log monitoring in " + this.GetType().Name + "...");
            ServiceAgent.StartWatchingFiles(debugMode,path, fileFilter, agentLogConsumer);
        }

        public void StopMonitoring()
        {
            Log.Debug("Stoppinging log monitoring in " + this.GetType().Name + "...");
            ServiceAgent.StopWatchingFiles();
        }
    }
}