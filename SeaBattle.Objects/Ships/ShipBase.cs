using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;
using SeaBattle.Service.ShipSupplies;

namespace SeaBattle.Service.Ships
{
    public abstract class ShipBase : IShip
    {
        #region Constructors

        protected ShipBase()
        {
            
        }

        #endregion

        #region Fields

        protected string Name;
        protected float ShipWeight;

        protected ShipCrew ShipCrew;
        protected Supplies ShipSupplies;

        #endregion

        #region Properties

        public bool SomethingChanged { get; set; }
        public bool IsStatic { get { return false; } }
        public abstract float Height { get; }
        public Vector2 Coordinates;

        public float FullWeight { get { return ShipWeight + ShipSupplies.ShipHold.LoadWeight; } }
        
        #endregion

        #region Methods

        protected abstract void InicializeFields();

        #endregion

        #region Serialization

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            //Recieve coordinates
            Coordinates.X = CommonSerializer.GetFloat(ref position, dataBytes);
            Coordinates.Y = CommonSerializer.GetFloat(ref position, dataBytes);
            ShipCrew.DeSerialize(ref position, dataBytes);
            ShipSupplies.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[]{0};
            var result = new byte[] {1};

            result = (byte[])result.Concat(BitConverter.GetBytes(Coordinates.X));
            result = (byte[])result.Concat(BitConverter.GetBytes(Coordinates.Y));
            result = (byte[])result.Concat(ShipCrew.Serialize());
            result = (byte[])result.Concat(ShipSupplies.Serialize());

            SomethingChanged = false;
            return result;
        }

        #endregion
    }
}
