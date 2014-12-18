using System.Collections.Generic;
using SeaBattle.Common.Objects;

namespace SeaBattle.Common.Session
{
    public class GameDescription
    {
        public GameDescription()
        {

        }

        public GameDescription(List<IPlayer> players, int maxPlayersAllowed, int gameId, int teams)
        {
            GameId = gameId;
            Players = players;
            MaximumPlayersAllowed = maxPlayersAllowed;
        }

        public int GameId { get; set; }

        public List<IPlayer> Players { get; set; }

        public int MaximumPlayersAllowed { get; set; }

        public override string ToString()
        {
            return string.Format("[ {0}/{1} ]", Players.Count, MaximumPlayersAllowed);
        }
    }
}
