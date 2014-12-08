using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;

namespace SeaBattle.Service.ShipSupplies
{
    class Bullet : IBullet, ISerializableObject
    {
        public bool IsStatic {get { return false; }}
        public int ID { get; private set; }
        public BulletType Type { get; private set; }
        public bool SomethingChanged { get; set; }
        public Vector2 CoordinatesFrom;
        public Vector2 CoordinatesTo;

        public Bullet(int id, BulletType bulletType)
        {
            ID = id;
            Type = bulletType;
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var result = new byte[]{};
            result = (byte[]) result.Concat(BitConverter.GetBytes(ID));
            result = (byte[]) result.Concat(BitConverter.GetBytes((byte)Type));
            

            return result;
        }
    }
}
