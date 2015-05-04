using System;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Service;

namespace SeaBattle.Common.Objects
{
    public interface IBullet : ICustomSerializable, IDisposable
    {
        BulletType Type { get; }
        Vector2 Coordinates { get; set; }
        string Shooter { get; }
        Vector2 CoordinatesFrom{ get; }
        Vector2 CoordinatesTo{ get; }
        bool IsStoped { get; set; }
    }
}
