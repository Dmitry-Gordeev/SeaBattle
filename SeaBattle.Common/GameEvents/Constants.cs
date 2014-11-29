using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.GameEvents
{
    public enum EventType
    {
        NewObjectEvent,
        ObjectDirectionChangedEvent,
        ObjectShootEvent,
        ObjectDeletedEvent,
        WeaponChangedEvent,
        ObjectHealthChanged,
        RequestForEvents
    }
}
