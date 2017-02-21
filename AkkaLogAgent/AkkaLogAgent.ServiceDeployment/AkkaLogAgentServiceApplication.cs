using AkkaLogAgent.AgentLogConsumerServices;
using AkkaLogAgent.DefaultLogFileHandler;
using AkkaLogAgent.Services;

namespace AkkaLogAgent.ServiceDeployment
{
    public class AkkaLogAgentServiceApplication
    {
        public AkkaLogAgentServiceApplication()
        {
            _deployedServiceAkkaLogAgentUi =
                new DeployedServiceAkkaLogAgentUI(new LogWatchServiceAgentService(new DefaultLogFileUpdateHandler()));
        }

        private readonly DeployedServiceAkkaLogAgentUI _deployedServiceAkkaLogAgentUi;

        public void Start()
        {
            _deployedServiceAkkaLogAgentUi.StartMonitoring(false, @"D:\Logs", "InventoryService*.log", new WinFormUiAgentLogConsumer());
        }

        public void Stop()
        {
            _deployedServiceAkkaLogAgentUi.StopMonitoring();
        }
    }
}