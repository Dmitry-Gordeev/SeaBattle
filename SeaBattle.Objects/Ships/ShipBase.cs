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

        protected ShipBase(IPlayer player)
        {
            Player = player;
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
        public object Lock { get; set; }
        public bool IsStatic { get { return false; } }
        public abstract float Height { get; }
        private Vector2 _coordinates;

        public float FullWeight { get { return ShipWeight + ShipSupplies.ShipHold.LoadWeight; } }

        public Vector2 Coordinates
        {
            get { return _coordinates; } 
            set { _coordinates = value; }
        }

        public IPlayer Player { get; set; }

        #endregion

        #region Methods

        protected abstract void InicializeFields();

        #endregion

        #region Serialization

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            Coordinates = CommonSerializer.GetVector2(ref position, dataBytes);
            ShipCrew.DeSerialize(ref position, dataBytes);
            ShipSupplies.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[]{0};
            var result = new byte[] {1};

            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Coordinates)).ToArray();
            result = result.Concat(ShipCrew.Serialize()).ToArray();
            result = result.Concat(ShipSupplies.Serialize()).ToArray();

            SomethingChanged = false;
            return result;
        }

        #endregion
    }
}
