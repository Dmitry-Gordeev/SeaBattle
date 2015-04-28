using System.Linq;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.ShipSupplies
{
    public class Compass : ICustomSerializable
    {
        public Vector2 Direction { get; set; }
        public bool SomethingChanged { get; set; }

        public Compass()
        {
            Direction = new Vector2();
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            //if (dataBytes[position++] == 0) return;
            Direction = CommonSerializer.GetVector2(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            var result = new byte[] { };
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Direction)).ToArray();
            return result;
        }
    }
}
