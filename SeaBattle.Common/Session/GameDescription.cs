using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SeaBattle.Common.Objects;

namespace SeaBattle.Common.Session
{
    [DataContract]
    public class GameDescription
    {
        public GameDescription(IEnumerable<IPlayer> players, int maxPlayersAllowed, int gameId, MapSet mapType, GameModes gameMode)
        {
            GameId = gameId;
            Players = new List<IPlayer> { };
            foreach (var player in players)
            {
                Players.Add(player);
            }
            MaximumPlayersAllowed = maxPlayersAllowed;
            MapType = mapType;
            GameMode = gameMode;
        }

        [DataMember]
        public int GameId { get; set; }

        [DataMember]
        public MapSet MapType { get; set; }

        [DataMember]
        public GameModes GameMode { get; set; }

        [DataMember]
        public List<IPlayer> Players { get; set; }

        [DataMember]
        public int MaximumPlayersAllowed { get; set; }

        public override string ToString()
        {
            var firstOrDefault = Players.FirstOrDefault();
            if (firstOrDefault != null)
                return string.Format("{0} [ {1}/{2} ]", firstOrDefault.Name ?? string.Empty, Players.Count, MaximumPlayersAllowed);
            return string.Empty;
        }
    }
}
