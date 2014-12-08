using System.Collections.Generic;

namespace SeaBattle.Common.Session
{
    public class GameDescription
    {
        public GameDescription()
        {

        }

        public GameDescription(List<string> players, int maxPlayersAllowed, int gameId)
        {
            GameId = gameId;
            Players = players;
            MaximumPlayersAllowed = maxPlayersAllowed;
        }

        public int GameId { get; set; }

        public List<string> Players { get; set; }

        public int MaximumPlayersAllowed { get; set; }

        public override string ToString()
        {
            return string.Format("[ {0}/{1} ]", Players.Count, MaximumPlayersAllowed);
        }
    }
}
