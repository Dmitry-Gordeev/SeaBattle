using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class ShipHold : ISerializableObject
    {
        public bool SomethingChanged { get; set; }
        public float LoadWeight
        {
            get { return 10f; }
        }


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
