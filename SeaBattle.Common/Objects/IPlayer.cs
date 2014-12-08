using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeaBattle.Common.Objects
{
    public interface IPlayer : ISerializableObject
    {
        int ID { get;}
        string Name { get;}
        IShip Ship { get; }
    }
}
