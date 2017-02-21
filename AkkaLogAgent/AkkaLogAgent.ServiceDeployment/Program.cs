using Topshelf;

namespace AkkaLogAgent.ServiceDeployment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AkkaLogAgentServiceApplication>(s =>
                {
                    s.ConstructUsing(name => new AkkaLogAgentServiceApplication());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.UseNLog();
                x.SetDescription("AkkaLogAgent Service");
                x.SetDisplayName("AkkaLogAgent Service");
                x.SetServiceName("AkkaLogAgentService");
            });
        }
    }
}