using System;
using System.Runtime.Serialization;
using SeaBattle.Common.Session;

namespace SeaBattle.Common.Objects
{
    public interface IPlayer
    {
        [DataMember]
        string Name { get; set; }
        [DataMember]
        string Login { get; }
        [DataMember]
        ShipTypes ShipType { get; set; }
        [DataMember]
        Guid ID { get; set; }
    }
}
