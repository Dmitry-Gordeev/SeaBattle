using System.Collections.Generic;
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

        private bool _firstSailsStateUp;
        private bool _secondSailsStateUp;
        
        public Sails()
        {
            SailsState = new byte[] {0, 0};
        }
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        #region Properties

        public byte[] SailsState { get; private set; }

        public float SailsSpeed
        {
            get { return (float) (SailsState[0] + SailsState[1]) / 100; }
        }

        #endregion

        #region Methods

        public void UpdateSailsState(GameEvent gameEvent)
        {
            switch (gameEvent.Type)
            {
                case (EventType.SailsUp):
                    if (!_firstSailsStateUp)
                    {
                        _firstSailsStateUp = true;
                        if (_firstSailsStateTimer != null)
                        {
                            _firstSailsStateTimer.Dispose();
                        }
                        _firstSailsStateTimer = new Timer(UpdateFirstSailsState, true, 0, 50);
                    }
                    else if (!_secondSailsStateUp)
                    {
                        _secondSailsStateUp = true;
                        if (_secondSailsStateTimer != null)
                        {
                            _secondSailsStateTimer.Dispose();
                        }
                        _secondSailsStateTimer = new Timer(UpdateSecondSailsState, true, 0, 50);
                    }
                    break;
                case (EventType.SailsDown):
                    if (_secondSailsStateUp)
                    {
                        _secondSailsStateUp = false;
                        if (_secondSailsStateTimer != null)
                        {
                            _secondSailsStateTimer.Dispose();
                        }
                        _secondSailsStateTimer = new Timer(UpdateSecondSailsState, false, 0, 50);
                    }
                    else if (_firstSailsStateUp)
                    {
                        _firstSailsStateUp = false;
                        if (_firstSailsStateTimer != null)
                        {
                            _firstSailsStateTimer.Dispose();
                        }
                        _firstSailsStateTimer = new Timer(UpdateFirstSailsState, false, 0, 50);
                    }
                    break;
            }
        }

        private void UpdateFirstSailsState(object isSailsUp)
        {
            if ((bool)isSailsUp)
            {
                if (SailsState[0] < 50)
                {
                    SailsState[0] += 1;
                }
                if (SailsState[0] < 50) return;
                SailsState[0] = 50;
                _firstSailsStateTimer.Dispose();
            }
            else
            {
                if (SailsState[0] > 0)
                {
                    SailsState[0] -= 1;
                }
                if (SailsState[0] > 0) return;
                SailsState[0] = 0;
                _firstSailsStateTimer.Dispose();
            }
        }

        private void UpdateSecondSailsState(object isSailsUp)
        {
            if ((bool)isSailsUp)
            {
                if (SailsState[1] < 50)
                {
                    SailsState[1] += 1;
                }
                if (SailsState[1] < 50) return;
                SailsState[1] = 50;
                _secondSailsStateTimer.Dispose();
            }
            else
            {
                if (SailsState[1] > 0)
                {
                    SailsState[1] -= 1;
                }
                if (SailsState[1] > 0) return;
                SailsState[1] = 0;
                _secondSailsStateTimer.Dispose();
            }
        }

        #endregion

        #region Serialization

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            SailsState[0] = dataBytes[position++];
            SailsState[1] = dataBytes[position++];
        }

        public IEnumerable<byte> Serialize()
        {
            return new byte[] { }.Concat(SailsState);
        }

        #endregion
    }
}
