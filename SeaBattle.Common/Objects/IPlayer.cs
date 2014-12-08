using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeaBattle.Common.Objects
{
    public interface IPlayer : ISerializableObject, IObject
    {
        string Name { get;}
        IShip Ship { get; }
    }
}
