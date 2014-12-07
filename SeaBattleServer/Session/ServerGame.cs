using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace SeaBattleServer.Session
{
    class ServerGame : Microsoft.Xna.Framework.Game
    {
        public ServerGame()
        {
            Content.RootDirectory = "Content";

            Components.Add(new SeaBattleGameComponent());

        }

        public void Start()
        {

        }
    }
}
