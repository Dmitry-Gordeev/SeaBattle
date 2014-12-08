using System;
using System.Runtime.Serialization;

namespace SeaBattle.Common.Objects
{
    public interface IShip : IObject, ISerializableObject
    {
        float FullWeight { get; }
    }
}
