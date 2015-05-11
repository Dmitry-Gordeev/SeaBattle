using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.ShipSupplies
{
    public class WindVane : ICustomSerializable
    {
        public Vector2 Direction;
        public bool SomethingChanged { get; set; }
        private double _angleOfDirection;
        private readonly Random _rnd = new Random();
        private Timer _updateDirectionTimer;
        public float ForceOfWind { get; private set; }

        // Надо ли запускать таймер апдейта состояния
        public WindVane(bool isNeedToSetTimer)
        {
            _angleOfDirection = _rnd.NextDouble() * 2 * Math.PI;
            //Direction = new Vector2((float)Math.Cos(_angleOfDirection), (float)Math.Sin(_angleOfDirection));
            if (isNeedToSetTimer)
            {
                _updateDirectionTimer = new Timer(UpdateAngleAndForce, null, 0, 60000);
            }
        }

        private void UpdateAngleAndForce(object obj)
        {
            _angleOfDirection = GetNextAngle();
            ForceOfWind = (float)(_rnd.NextDouble() * 9 + 1);
            UpdateDirection();
        }

        private void UpdateDirection()
        {
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
            ForceOfWind = CommonSerializer.GetFloat(ref position, dataBytes);
            UpdateDirection();
        }

        public IEnumerable<byte> Serialize()
        {
            var result = new byte[] {}.Concat(BitConverter.GetBytes(_angleOfDirection));
            return result.Concat(BitConverter.GetBytes(ForceOfWind));
        }
    }
}
