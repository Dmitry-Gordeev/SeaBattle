using System.Linq;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Sails : ICustomSerializable
    {
        public Sails()
        {
            _sailsState = new byte[] {0, 0};
        }
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        private byte[] _sailsState;

        #region Methods

        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            _sailsState[0] = dataBytes[position++];
            _sailsState[1] = dataBytes[position++];
        }

        public byte[] Serialize()
        {
            //if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { };

            result = result.Concat(_sailsState).ToArray();

            return result;
        }
    }
}
