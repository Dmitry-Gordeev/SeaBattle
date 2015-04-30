using System.Runtime.Serialization;
using SeaBattle.Common.Session;

namespace SeaBattle.Common
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public ShipType ShipType { get; set; }

        #region Constructors

        public Player(string name, ShipType shipType)
        {
            Login = name;
            Name = name;
            ShipType = shipType;
        }

        public Player(string name, string login , ShipType shipType)
        {
            Login = login;
            Name = name;
            ShipType = shipType;
        }

        #endregion
    }
}
