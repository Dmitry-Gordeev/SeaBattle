using System;
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
        [DataMember]
        public Guid ID { get; set; }

        #region Constructors

        public Player(string name, ShipType shipType, Guid id)
        {
            Login = name;
            Name = name;
            ShipType = shipType;
            ID = id;
        }

        public Player()
        {
            Name = string.Empty;
            Login = string.Empty;
            ShipType = ShipType.Lugger;
            ID = new Guid();
        }

        public Player(string name, string login, ShipType shipType, Guid id)
        {
            Login = login;
            Name = name;
            ShipType = shipType;
            ID = id;
        }

        #endregion
    }
}
