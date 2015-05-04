using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;
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

        public Player Player { get; set; }

        private readonly InstanceContext _channelContext;

        private readonly Queue<GameEvent> _filteredEvents = new Queue<GameEvent>();

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
                Player = new Player(username, ShipType.Lugger, Guid.NewGuid());
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
            var gameDescription = new GameDescription(new List<Player> { Player }, maxPlayers, _globalGameID++, mapType, mode);
            GamesList.Add(gameDescription);

            return gameDescription.GameId;
        }

        public bool JoinGame(int gameId)
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == gameId);

            if (currentGame == null || currentGame.Players.Count >= currentGame.MaximumPlayersAllowed) return false;

            _currentGameId = gameId;
            currentGame.Players.Add(Player);
            return true;
        }

        public bool IsHost()
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);
            if (currentGame == null) return false;
            return Player.Name == currentGame.Host.Name;
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
                if (Player != null) currentGame.Players.Remove(Player);
                Console.WriteLine("LeaveGame with localID: " + _localID + "\nAnd GameID: " + _currentGameId);
                if (currentGame.Players.Count == 0)
                {
                    GamesList.Remove(currentGame);
                    Console.WriteLine("Game: " + _currentGameId + " closed");
                }
            }
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
        
        public byte[] GetInfo()
        {
            return _currentGameId == -1 ? null : SessionManager.Instance.GetInfo(_currentGameId);
        }

        public List<Player> PlayerListUpdate()
        {
            var currentGame = GamesList.FirstOrDefault(game => game.GameId == _currentGameId);

            return currentGame != null ? currentGame.Players : null;
        }

        public void AddClientGameEvent(GameEvent gameEvent)
        {
            Console.WriteLine(gameEvent.Type);
            
            if (_currentGameId == -1)
                return;

            SessionManager.Instance.HandleGameEvent(gameEvent, Player.Name, _currentGameId);
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
