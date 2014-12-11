using System;
using SeaBattle.Common.Objects;
using SeaBattle.Service.Ships;

namespace SeaBattle.Service.Player
{
    public class Player : IPlayer
    {
        public bool SomethingChanged { get; set; }
        public bool IsStatic { get; private set; }
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

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            Ship.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            return Ship.Serialize();
        }
        
        #endregion
    }
}
