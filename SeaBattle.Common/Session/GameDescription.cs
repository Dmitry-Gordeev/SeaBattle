using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SeaBattle.Common.Session
{
    [DataContract]
    public class GameDescription
    {
        public GameDescription(IEnumerable<Player> players, int maxPlayersAllowed, int gameId, MapSet mapType, GameModes gameMode)
        {
            if (players == null)
                return;
            GameId = gameId;
            Players = new List<Player> { };
            var enumerable = players as IList<Player> ?? players.ToList();
            Host = enumerable.FirstOrDefault();
            foreach (var player in enumerable)
            {
                Players.Add(player);
            }
            MaximumPlayersAllowed = maxPlayersAllowed;
            MapType = mapType;
            GameMode = gameMode;
            IsGameStarted = false;
        }

        [DataMember]
        public int GameId { get; set; }

        [DataMember]
        public MapSet MapType { get; set; }

        [DataMember]
        public GameModes GameMode { get; set; }

        [DataMember]
        public Player Host { get; set; }

        [DataMember]
        public List<Player> Players { get; set; }

        [DataMember]
        public int MaximumPlayersAllowed { get; set; }

        [DataMember]
        public bool IsGameStarted { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [ {1}/{2} ]", Host.Name, Players.Count, MaximumPlayersAllowed);
        }
    }
}
