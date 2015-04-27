using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Session;

namespace SeaBattle.Service.Session
{
    class SessionManager
    {
        private static readonly SessionManager LocalInstance = new SessionManager();

        /// <summary>
        /// Содержит текущие игры
        /// </summary>
        private readonly List<GameSession> _gameSessions;

        /// <summary>
        /// Уникальный идентификатор, который присваивается каждой игре при её создании
        /// </summary>
        private int _gameId;

        public Dictionary<Guid, GameSession> SessionTable;

        private SessionManager()
        {
            SessionTable = new Dictionary<Guid, GameSession>();
            _gameSessions = new List<GameSession>();
            _gameId = 1;
        }

        public static SessionManager Instance
        {
            get { return LocalInstance; }
        }

        /// <summary>
        /// Добавляем игрока в текущую игру.
        /// </summary>
        public bool JoinGame(GameDescription game, SeaBattleService player)
        {
            GameSession session = _gameSessions.Find(curGame => curGame.LocalGameDescription.GameId == game.GameId);
            return false;
        }

        /// <summary>
        /// Создаем новую игру
        /// </summary>
        public GameDescription CreateGame(GameModes modes, int maxPlayers, SeaBattleService client, int teams)
        {
            GameSession gameSession;

            

            return null;
        }

        /// <summary>
        /// Возвращает список игр.
        /// </summary>
        public GameDescription[] GetGameList()
        {
            var gameSessions = _gameSessions.ToArray();

            return (from t in gameSessions where !t.IsStarted select t.LocalGameDescription).ToArray();
        }

        /// <summary>
        /// Ищем игру, в которой находится игрок и удаляем его оттуда.
        /// </summary>
        public bool LeaveGame(SeaBattleService player)
        {
            try
            {
                GameSession game;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GameLevel GameStarted(int gameId)
        {
            var game = _gameSessions.Find(x => x.LocalGameDescription.GameId == gameId);
            return game != null ? (game.IsStarted ? game.GameLevel : null) : null;
        }
    }
}
