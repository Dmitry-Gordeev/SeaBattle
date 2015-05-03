using System;
using System.Linq;
using System.Threading;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Sails : ICustomSerializable
    {
        private Timer _firstSailsStateTimer;
        private Timer _secondSailsStateTimer;
        
        public Sails()
        {
            _sailsState = new byte[] {0, 0};
        }
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        private byte[] _sailsState;

        #region Properties



        #endregion

        #region Methods

        public void UpdateSailsState(GameEvent gameEvent)
        {
            switch (gameEvent.Type)
            {
                case (EventType.SailsUp):

                    break;
                case (EventType.SailsDown):
                    break;
            }
        }

        private void UpdateFirstSailsState(object gameEvent)
        {
            
        }

        private void UpdateSecondSailsState(object gameEvent)
        {

        }

        #endregion

        #region Serialization

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

        #endregion
    }
}
