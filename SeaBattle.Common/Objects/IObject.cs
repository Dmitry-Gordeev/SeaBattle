using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    public interface IObject
    {
        bool IsStatic { get; }
        int ID { get; }
    }
}
