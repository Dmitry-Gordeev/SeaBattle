using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeaBattle.Common.GameEvents
{
    public class AGameEvent
    {
        public long TimeStamp { get; private set; }

        public Guid? GameObjectId { get; private set; }

        public EventType Type;

        protected AGameEvent(Guid? id, long timeStamp)
        {
            GameObjectId = id;
            TimeStamp = timeStamp;
        }
    }
}
