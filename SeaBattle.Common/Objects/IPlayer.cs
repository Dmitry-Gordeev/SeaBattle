using SeaBattle.Common.Session;

namespace SeaBattle.Common.Objects
{
    public interface IPlayer
    {
        string Name { get; set; }
        string Login { get; }
        ShipTypes ShipType { get; set; }
    }
}
