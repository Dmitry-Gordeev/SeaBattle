using System;
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
        
        protected int Rowers;
        protected int Gunners;
        protected int Sailors;
        protected int PirateFighters;
        protected Supplies ShipSupplies;

        #endregion

        #region Properties

        public bool IsStatic { get { return false; } }
        public abstract float Height { get; }
        public Vector2 Coordinates { get; set; }

        public int NumberOfPeople
        {
            get
            {
                return Rowers + Gunners + Sailors + PirateFighters;
            }
        }
        
        public float LoadWeight
        {
            get { return 10f; }
        }

        public float FullWeight { get { return ShipWeight + LoadWeight; } }

        #endregion

        #region Methods

        protected abstract void InicializeFields();

        #endregion

        #region Serialization

        public object DeSerialize(ref long position)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize(ref long position)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
