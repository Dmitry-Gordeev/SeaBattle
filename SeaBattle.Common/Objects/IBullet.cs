using Microsoft.Xna.Framework;
using SeaBattle.Common.Service;

namespace SeaBattle.Common.Objects
{
    public interface IBullet : ICustomSerializable, IObject
    {
        BulletType Type { get; }
        Vector2 Coordinates { get; set; }
    }
}
