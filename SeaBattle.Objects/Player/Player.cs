using System.Runtime.Serialization;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Session;

namespace SeaBattle.Service.Player
{
    [DataContract]
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public string Login { get; private set; }
        public ShipTypes ShipType { get; set; }

        #region Constructors

        public Player(string name, ShipTypes shipType)
        {
            Login = name;
            Name = name;
            ShipType = shipType;
        }

        #endregion
    }
}
