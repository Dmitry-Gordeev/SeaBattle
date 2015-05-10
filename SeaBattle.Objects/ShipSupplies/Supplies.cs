using System.Collections.Generic;
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

        public IEnumerable<byte> Serialize()
        {
            //if (!SomethingChanged) return new byte[] { 0 };
            
            //result = result.Concat(Cannons.Serialize());
            //result = result.Concat(ShipHold.Serialize());
            var result = new byte[] { 1 }.Concat(Sails.Serialize());

            SomethingChanged = false;
            return result;
        }
    }
}
