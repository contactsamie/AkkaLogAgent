namespace AkkaLogAgent.Common
{
    public interface IAkkaLogAgentUI
    {
        void StartMonitoring(bool debugMode, string path, string fileFilter, IAgentLogConsumer agentLogConsumer);

        void StopMonitoring();
    }
}