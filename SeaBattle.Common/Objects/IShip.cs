using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IShip : ICustomSerializable, IObject
    {
        float FullWeight { get; }
        Vector2 Coordinates { get; set; }
        IPlayer Player { get; set; }
    }
}
