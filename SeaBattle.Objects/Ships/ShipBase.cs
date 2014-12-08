using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
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
        public Vector2 Coordinates { get; set; }

        public float FullWeight { get { return ShipWeight + ShipSupplies.ShipHold.LoadWeight; } }

        #endregion

        #region Methods

        protected abstract void InicializeFields();

        #endregion

        #region Serialization

        public object DeSerialize(ref long position, byte[] dataBytes)
        {
            throw new NotImplementedException();
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
