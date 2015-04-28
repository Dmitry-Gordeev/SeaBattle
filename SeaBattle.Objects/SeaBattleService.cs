using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Service.DA;
using SeaBattle.Service.Session;

namespace SeaBattle.Service
{
    public class SeaBattleService : ISeaBattleService
    {
        #region private fields

        private static readonly List<SeaBattleService> ClientsList = new List<SeaBattleService>();
        private static readonly List<GameDescription> GamesList = new List<GameDescription>();

        private static int _globalServiceID;
        private static int _globalGameID;

        private readonly int _localID;
        private int _currentGameId = -1;

        public IPlayer Player { get; set; }

        private readonly InstanceContext _channelContext;

        private readonly Queue<AGameEvent> _filteredEvents = new Queue<AGameEvent>();

        #endregion

        public SeaBattleService()
		{
			_channelContext = OperationContext.Current.InstanceContext;
			_channelContext.Faulted += OnChannelStopped;
			_channelContext.Closed += OnChannelStopped;
			_localID = _globalServiceID;
            ClientsList.Add(this);
            Console.WriteLine("Global ID: " + _globalServiceID);
            Console.WriteLine("ClientsList count: " + ClientsList.Count);
            _globalServiceID++;
		}

        #region регистрация, аутентификация

        public AccountManagerErrorCode Register(string username, string password)
        {
            return DataBaseAdapter.Instance.RegisterPlayer(username, password);
        }

        public AccountManagerErrorCode Login(string username, string password)
        {
            var errorCode = DataBaseAdapter.Instance.GetPlayerStatus(username, password);
            if (errorCode == AccountManagerErrorCode.Ok)
            {
                Player = new Player.Player(username, ShipType.Lugger);
            }
            Console.WriteLine("Player name: " + Player.Name + " entered");
            return errorCode;
        }

        public void Logout()
        {
            Console.WriteLine("Logout");
            _channelContext.Abort();
        }

        #endregion

        #region Основные методы инициализации игры

        public List<GameDescription> GetGameList()
        {
            return GamesList;
        }

        public int CreateGame(GameModes mode, int maxPlayers, MapSet mapType)
        {
            _currentGameId = _globalGameID;
            var gameDescription = new GameDescription(new List<IPlayer> { Player }, maxPlayers, _globalGameID++, mapType, mode);
            GamesList.Add(gameDescription);

            return gameDescription.GameId;
        }

        public bool JoinGame(int gameId)
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == gameId);

            if (currentGame == null || currentGame.Players.Count >= currentGame.MaximumPlayersAllowed) return false;

            _currentGameId = gameId;
            currentGame.Players.Add(Player.Name);
            return true;
        }

        public bool IsHost()
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);
            if (currentGame == null) return false;
            return Player.Name == currentGame.Host;
        }

        public void LeaveGame()
        {
            if (Player != null)
            {
                Console.WriteLine("Player " + Player.Name + " leave");
            }
            if (_currentGameId == -1)
            {
                Console.WriteLine("Player with localID: " + _localID + "\nNot in game");
                return;
            }
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);
            if (currentGame != null)
            {
                if (Player != null) currentGame.Players.Remove(Player.Name);
                if (currentGame.Players.Count == 0)
                {
                    GamesList.Remove(currentGame);
                }
            }

            Console.WriteLine("LeaveGame with localID: " + _localID + "\nAnd GameID: " + _currentGameId );
            _currentGameId = -1;
        }

        public byte[] IsGameStarted(int gameId)
        {
            return SessionManager.Instance.IsGameStarted(gameId);
        }

        public bool StartGameSession()
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);
            if (currentGame != null)
                SessionManager.Instance.CreateGame(currentGame);
            return true;
        }

        #endregion

        #region процесс игры

        public long GetServerGameTime()
        {
            throw new NotImplementedException();
        }

        public List<string> PlayerListUpdate()
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);

            return currentGame != null ? currentGame.Players : null;
        }

        public AGameEvent[] GetEvents()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Контроль за соединением

        void OnChannelStopped(object sender, EventArgs e)
        {
            _channelContext.Faulted -= OnChannelStopped;
            _channelContext.Closed -= OnChannelStopped;
            ClientsList.RemoveAll(service => service._localID == _localID);

            LeaveGame();
        }

        #endregion
    }
}
