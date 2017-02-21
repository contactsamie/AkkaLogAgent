using System.Collections.Generic;

namespace AkkaLogAgent.Common
{
    public interface ILogFileUpdateHandler
    {
        void Reset();

        void HandleFileChangeEvent(List<IAgentLogConsumer> consumers, string path);
    }
}