using System;
using System.Linq;
using System.Text;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;
using SeaBattle.Service.Ships;

namespace SeaBattle.Service.Player
{
    public class Player : IPlayer
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
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
            Name = CommonSerializer.GetString(ref position, dataBytes);
            Ship.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            var result = new byte[]{};
            result = result.Concat(CommonSerializer.StringToBytesArr(Name)).ToArray();
            result = result.Concat(Ship.Serialize()).ToArray();

            return result;
        }
        
        #endregion
    }
}
