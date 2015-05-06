using System;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Utils;
using XnaAdapter;

namespace SeaBattle.Service.ShipSupplies
{
    public class Bullet : IBullet
    {
        public BulletType Type { get; private set; }
        public Vector2 Coordinates { get; set; }
        public string Shooter { get; private set; }
        public Vector2 CoordinatesFrom { get; set; }
        public Vector2 CoordinatesTo { get; set; }
        public bool IsStoped { get; set; }
        public float Damage { get ; private set; }

        private readonly Timer _movingTimer;
        private Vector2 _direction;

        public Bullet()
        {
            
        }

        public Bullet(BulletType bulletType, string shooter, Vector2 coordinatesFrom, Vector2 coordinatesTo)
        {
            Shooter = shooter;
            Type = bulletType;
            CoordinatesFrom = coordinatesFrom;
            CoordinatesTo = coordinatesTo;
            Coordinates = CoordinatesFrom;
            Damage = 10f;
            _direction = PolarCoordinateHelper.GetDirection(CoordinatesFrom, CoordinatesTo);
            _direction.Normalize();

            _movingTimer = new Timer(Move, null, 0, 20);
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            //Type = (BulletType)dataBytes[position++];
            Shooter = CommonSerializer.GetString(ref position, dataBytes);
            Coordinates = CommonSerializer.GetVector2(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            var result = new byte[]{};
            //result = result.Concat(BitConverter.GetBytes((byte)Type)).ToArray();
            result = result.Concat(CommonSerializer.StringToBytesArr(Shooter)).ToArray();
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Coordinates)).ToArray();

            return result;
        }

        private void Move(object obj)
        {
            if (PolarCoordinateHelper.GetDirection(Coordinates, CoordinatesTo).X * _direction.X <= 0 ||
                PolarCoordinateHelper.GetDirection(Coordinates, CoordinatesTo).Y * _direction.Y <= 0)
            {
                Dispose();
                return;
            }
            Coordinates += _direction*10;
        }

        public void Dispose()
        {
            if (_movingTimer != null)
            {
                _movingTimer.Dispose();
            }
            IsStoped = true;
        }
    }
}
