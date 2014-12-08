using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xna.Framework.Net;
using SeaBattle.Common.Service;
using SeaBattle.Service;

namespace SeaBattleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = new ServiceHost(typeof(SeaBattleService), new Uri("net.tcp://127.0.0.1:4721"));

                host.AddServiceEndpoint(typeof(ISeaBattleService), new NetTcpBinding(SecurityMode.None)
                {
                    ReceiveTimeout = new TimeSpan(0, 0, 0, 10),
                    CloseTimeout = new TimeSpan(0, 0, 0, 10),
                    OpenTimeout = new TimeSpan(0, 0, 0, 10),
                    SendTimeout = new TimeSpan(0, 0, 0, 10),
                }, "SeaBattleService");
                host.CloseTimeout = new TimeSpan(0, 0, 0, 10);
                host.Closed += new EventHandler(host_Closed);
                host.Faulted += new EventHandler(host_Faulted);
                var metadataBehavior =
                        host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (metadataBehavior == null)
                {
                    metadataBehavior = new ServiceMetadataBehavior { HttpGetEnabled = false };
                    host.Description.Behaviors.Add(metadataBehavior);
                }

                host.AddServiceEndpoint(typeof(IMetadataExchange),
                        MetadataExchangeBindings.CreateMexTcpBinding(),
                        "mex");

                host.Open();
                Console.WriteLine("Started!");
                while (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                }
                host.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Server crashed: " + e);
                throw;
            }
        }

        private static NetworkSessionType GetNetworkSessionType(bool isLocalGame)
        {
            return isLocalGame ? NetworkSessionType.Local : NetworkSessionType.SystemLink;
        }

        static void host_Faulted(object sender, EventArgs e)
        {

        }

        static void host_Closed(object sender, EventArgs e)
        {

        }
    }
}
