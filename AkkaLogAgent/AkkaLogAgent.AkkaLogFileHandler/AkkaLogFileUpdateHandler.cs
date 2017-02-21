using AkkaLogAgent.Common;
using System;
using System.Collections.Generic;

namespace AkkaLogAgent.AkkaLogFileHandler
{
    public class AkkaLogFileUpdateHandler : ILogFileUpdateHandler
    {
        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void HandleFileChangeEvent(List<IAgentLogConsumer> consumers, string path)
        {
            throw new NotImplementedException();
        }
    }
}