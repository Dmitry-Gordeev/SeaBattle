using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace SeaBattleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NetworkSessionType sessionType = GetNetworkSessionType(false);
                byte maxLocalGamers = 8;
                byte maxGamers = 8;
                var networkSession = NetworkSession.Create(sessionType, maxLocalGamers, maxGamers);

                networkSession.AllowHostMigration = true;
                networkSession.AllowJoinInProgress = true;

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
    }
}
