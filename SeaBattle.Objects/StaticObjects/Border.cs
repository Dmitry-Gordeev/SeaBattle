using System.Linq;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.StaticObjects
{
    public class Border : IStaticObject
    {
        public Border(Side side)
        {
            switch (side)
            {
                case Side.Top:
                {
                    Coordinates = new Vector2(Constants.LevelWidth / 2, 50);
                    break;
                }
                case Side.Right:
                {
                    Coordinates = new Vector2(Constants.LevelWidth - 50, Constants.LevelHeigh / 2);
                    break;
                }
                case Side.Bottom:
                {
                    Coordinates = new Vector2(Constants.LevelWidth / 2, Constants.LevelHeigh - 50);
                    break;
                }
                case Side.Left:
                {
                    Coordinates = new Vector2(50, Constants.LevelHeigh / 2);
                    break;
                }
            }
        }


        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            Coordinates = CommonSerializer.GetVector2(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            var result = new byte[] { };
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Coordinates)).ToArray();
            return result;
        }

        public Vector2 Coordinates { get; set; }
    }
}
