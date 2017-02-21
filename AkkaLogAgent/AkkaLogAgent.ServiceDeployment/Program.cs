using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AkkaLogAgent.ServiceDeployment
{
   public class Program
    {
      public  static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 //1
            {
                x.Service<AkkaLogAgentServiceApplication>(s =>                        //2
                {
                    s.ConstructUsing(name => new AkkaLogAgentServiceApplication());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6
                x.UseNLog();
                x.SetDescription("AkkaLogAgent Service");        //7
                x.SetDisplayName("AkkaLogAgent Service");                       //8
                x.SetServiceName("AkkaLogAgentService");                   //9
            });
        }
    }
}
