using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IStaticObject : ICustomSerializable
    {
        Vector2 Coordinates { get; set; }
    }
}
