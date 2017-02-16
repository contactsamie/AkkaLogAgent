namespace AkkaLogAgent.Services
{
    public interface IAgentLogConsumer
    {
        void OnEachLogUpdate(string logUpdate);
        void OnBatchLogUpdate(string batchLogUpdate );
    }
}