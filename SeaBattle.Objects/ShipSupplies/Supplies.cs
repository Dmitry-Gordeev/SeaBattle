using SeaBattle.Common.Objects;
using System.Linq;

namespace SeaBattle.Service.ShipSupplies
{
    public class Supplies : ICustomSerializable
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        public Supplies(Cannons cannons, ShipHold shipHold, Sails sails, WindVane windVane)
        {
            Cannons = cannons;
            ShipHold = shipHold;
            Sails = sails;
            WindVane = windVane;
        }

        #region Serializable

        public Cannons Cannons;
        public ShipHold ShipHold;
        public Sails Sails;

        #endregion

        #region Not serializable

        public readonly WindVane WindVane;
       
        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            //Cannons.DeSerialize(ref position, dataBytes);
            //ShipHold.DeSerialize(ref position, dataBytes);
            Sails.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            //if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            //result = result.Concat(Cannons.Serialize()).ToArray();
            //result = result.Concat(ShipHold.Serialize()).ToArray();
            result = result.Concat(Sails.Serialize()).ToArray();

            SomethingChanged = false;
            return result;
        }
    }
}
