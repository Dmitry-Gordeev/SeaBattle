using System;
using System.Linq;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Sails : ICustomSerializable
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        private byte _sailsState;

        #region Methods

        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            _sailsState = dataBytes[position++];
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = result.Concat(new Byte[]{_sailsState}).ToArray();

            return result;
        }
    }
}
