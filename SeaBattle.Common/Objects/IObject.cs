using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IObject
    {
        bool IsStatic { get; }
        float Height { get; }
        Vector2 Coordinates { get; set; }
    }
}
