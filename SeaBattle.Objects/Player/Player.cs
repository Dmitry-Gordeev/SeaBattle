using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;
using SeaBattle.Common.Objects;

namespace SeaBattle.Objects.Player
{
    public class Player : IPlayer
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public IShip Ship { get; private set; }

        #region Constructors

        public Player(IShip ship)
        {
            Ship = ship;
        }

        #endregion

        #region Serialization

        public object DeSerialize(ref long position)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize(ref long position)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
