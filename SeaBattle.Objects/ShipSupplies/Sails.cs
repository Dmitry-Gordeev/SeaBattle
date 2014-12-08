using System;
using System.Linq;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Sails : ISerializableObject
    {
        public bool SomethingChanged { get; set; }

        private byte _sailsState;

        #region Methods

        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = (byte[]) result.Concat(new Byte[]{_sailsState});

            return result;
        }
    }
}
