namespace AkkaLogAgent.Common
{
    public interface IAgentLogConsumer
    {
        void OnEachLogUpdate(string logUpdate);

        void OnBatchLogUpdate(string batchLogUpdate);

        void OnStoped();

        void OnStarted();
        void Start(bool debugMode);
    }
}