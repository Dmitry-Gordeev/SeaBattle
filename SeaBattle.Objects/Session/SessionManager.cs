using System;
using System.Collections.Generic;
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

        private SessionManager()
        {
            _gameSessions = new List<GameSession>();
        }

        public static SessionManager Instance
        {
            get { return LocalInstance; }
        }

        /// <summary>
        /// Создаем новую игру
        /// </summary>
        public GameDescription CreateGame(GameDescription gameDescription)
        {
            var gameSession = new GameSession(gameDescription);
            _gameSessions.Add(gameSession);

            return null;
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

        public bool GameStarted(int gameId)
        {
            var game = _gameSessions.Find(x => x.LocalGameDescription.GameId == gameId);
            return game != null;
        }
    }
}
