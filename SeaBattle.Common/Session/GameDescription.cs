using System.Collections.Generic;

namespace SeaBattle.Common.Session
{
    public class GameDescription
    {
        public GameDescription()
        {
        }

        public GameDescription(List<string> players, int maxPlayersAllowed, int gameId, int teams)
        {
            GameId = gameId;
            Players = players;
            MaximumPlayersAllowed = maxPlayersAllowed;
            Teams = teams;
        }

        public int GameId { get; set; }

        public List<string> Players { get; set; }

        public int MaximumPlayersAllowed { get; set; }

        public int Teams { get; set; }

        public override string ToString()
        {
            return string.Format("[ {0}/{1} ; {2}]", Players.Count, MaximumPlayersAllowed, Teams);
        }
    }
}
