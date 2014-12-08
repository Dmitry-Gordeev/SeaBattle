﻿using SeaBattle.Common.Objects;
using System.Linq;

namespace SeaBattle.Service.ShipSupplies
{
    public class Supplies : ISerializableObject
    {
        public bool SomethingChanged { get; set; }

        #region Serializable

        public Cannons Cannons;
        public ShipHold ShipHold;
        public Sails Sails;

        #endregion

        #region Not serializable

        public Compass Compass;
        public WindVane WindVane;
       
        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = (byte[])result.Concat(Cannons.Serialize());
            result = (byte[])result.Concat(ShipHold.Serialize());
            result = (byte[])result.Concat(Sails.Serialize());

            SomethingChanged = false;
            return result;
        }
    }
}
