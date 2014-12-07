using System.Runtime.Serialization;

namespace SeaBattle.Common.Objects
{
    public interface IShip : IObject, ISerializableObject
    {
        int NumberOfPeople { get; }
        float LoadWeight { get; }
        float FullWeight { get; }
    }
}
