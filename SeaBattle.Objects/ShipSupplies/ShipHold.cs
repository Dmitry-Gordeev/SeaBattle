using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Objects;
using SeaBattle.Service.Goods;

namespace SeaBattle.Service.ShipSupplies
{
    public class ShipHold : ISerializableObject
    {
        public bool SomethingChanged { get; set; }
        private List<Good> _goods;

        #region Properties

        public float LoadWeight
        {
            get
            {
                return Goods.Sum(good => good.FullWeight);
            }
        }

        public List<Good> Goods
        {
            get { return _goods; }
            private set { _goods = value; }
        }

        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = (byte[])result.Concat(BitConverter.GetBytes(Goods.Count));

            foreach (Good good in Goods)
            {
                result = (byte[]) result.Concat(good.Serialize());
            }

            SomethingChanged = false;
            return result;
        }
    }
}
