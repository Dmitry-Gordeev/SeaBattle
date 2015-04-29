using System;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.ShipSupplies
{
    public class Compass : ICustomSerializable
    {
        public Vector2 Direction;
        public bool SomethingChanged { get; set; }
        private double _angleOfDirection;
        private readonly Random _rnd = new Random();
        private Timer _updateDirectionTimer;

        public Compass(bool isNeedToSetTimer)
        {
            _angleOfDirection = _rnd.NextDouble() * 2 * Math.PI;
            Direction = new Vector2((float)Math.Cos(_angleOfDirection), (float)Math.Sin(_angleOfDirection));
            if (isNeedToSetTimer)
            {
                _updateDirectionTimer = new Timer(UpdateDirection, null, 1000, 1000);
            }
        }

        private void UpdateDirection(object obj)
        {
            _angleOfDirection = GetNextAngle();
            Direction.X = (float)Math.Cos(_angleOfDirection);
            Direction.Y = (float)Math.Sin(_angleOfDirection);
        }

        private double GetNextAngle()
        {
            return _rnd.NextDouble() * 2 * Math.PI;
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            _angleOfDirection = CommonSerializer.GetDouble(ref position, dataBytes);
            Direction = CommonSerializer.GetVector2(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            var result = new byte[] { };
            result = result.Concat(BitConverter.GetBytes(_angleOfDirection)).ToArray();
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Direction)).ToArray();
            return result;
        }
    }
}
