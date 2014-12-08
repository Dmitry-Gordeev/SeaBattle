using System;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.Player
{
    public class Player : IPlayer
    {
        public bool SomethingChanged { get; set; }
        public int ID { get; private set; }
        public string Name { get; private set; }
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

        public byte[] Serialize()
        {
            return Ship.Serialize();
        }
        
        #endregion
    }
}
