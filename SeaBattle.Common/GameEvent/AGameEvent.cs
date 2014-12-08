using System;

namespace SeaBattle.Common.GameEvent
{
    public abstract class AGameEvent
    {
        public long TimeStamp { get; private set; }
        public Guid? GameObjectId { get; private set; }
        public EventType Type;

        protected AGameEvent(Guid? id, long timeStamp, EventType type)
        {
            GameObjectId = id;
            TimeStamp = timeStamp;
            Type = type;
        }
    }
}
