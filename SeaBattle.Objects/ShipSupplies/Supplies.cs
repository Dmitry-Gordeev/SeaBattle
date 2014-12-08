using SeaBattle.Common.Objects;
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

        public WindVane WindVane;
       
        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            Cannons.DeSerialize(ref position, dataBytes);
            ShipHold.DeSerialize(ref position, dataBytes);
            Sails.DeSerialize(ref position, dataBytes);
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
