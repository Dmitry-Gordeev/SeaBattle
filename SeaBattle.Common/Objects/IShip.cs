using System;
using System.Runtime.Serialization;

namespace SeaBattle.Common.Objects
{
    public interface IShip : ISerializableObject
    {
        float FullWeight { get; }
    }
}
