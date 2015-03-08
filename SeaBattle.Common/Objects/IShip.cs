using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IShip : ISerializableObject
    {
        float FullWeight { get; }
        Vector2 Coordinates { get; set; }
    }
}
