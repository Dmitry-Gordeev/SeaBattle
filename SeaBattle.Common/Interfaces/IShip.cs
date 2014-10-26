namespace SeaBattle.Common.Interfaces
{
    public interface IShip : IObject
    {
        int NumberOfPeople { get; }
        float LoadWeight { get; }
        float FullWeight { get; }
    }
}
