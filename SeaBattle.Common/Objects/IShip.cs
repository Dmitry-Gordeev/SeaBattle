using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IShip : ICustomSerializable
    {
        float FullWeight { get; }
        Vector2 Coordinates { get; set; }
        Player Player { get; set; }
    }
}
