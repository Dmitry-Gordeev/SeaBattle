using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Sails : ISerializableObject
    {
        public bool SomethingChanged { get; set; }

        public object DeSerialize(ref long position, byte[] dataBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
