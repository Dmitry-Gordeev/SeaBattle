using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;
using SeaBattle.Service.Goods;

namespace SeaBattle.Service.ShipSupplies
{
    public class ShipHold : ICustomSerializable
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
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
            if (dataBytes[position++] == 0) return;

            var count = CommonSerializer.GetInt(ref position, dataBytes);
            Goods = new List<Good>();

            for (int i = 0; i < count; i++)
            {
                var tmpGood = new Good();
                tmpGood.DeSerialize(ref position, dataBytes);
                Goods.Add(tmpGood);
            }
        }

        public byte[] Serialize()
        {
            //if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = result.Concat(BitConverter.GetBytes(Goods.Count)).ToArray();

            for (int i = 0; i < Goods.Count; i++)
            {
                result = result.Concat(Goods[i].Serialize()).ToArray();
            }

            SomethingChanged = false;
            return result;
        }
    }
}
