using System;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.Player
{
    public class Player : IPlayer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IShip Ship { get; private set; }

        #region Constructors

        public Player(IShip ship, int id, string name )
        {
            Ship = ship;
            ID = id;
            Name = name;
        }

        #endregion

        #region Serialization

        public object DeSerialize(ref long position, byte[] dataBytes)
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
