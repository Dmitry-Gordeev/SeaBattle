﻿using System;
using System.Collections.Generic;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Session;

namespace SeaBattle.Service.Session
{
    class SessionManager
    {
        private static SessionManager _localInstance = new SessionManager();

        /// <summary>
        /// Содержит текущие стартовавшие игры
        /// </summary>
        private readonly List<GameSession> _gameSessions;

        private SessionManager()
        {
            _gameSessions = new List<GameSession>();
        }

        public static SessionManager Instance
        {
            get { return _localInstance ?? (_localInstance = new SessionManager()); }
        }

        /// <summary>
        /// Создаем новую игру
        /// </summary>
        public void CreateGame(GameDescription gameDescription)
        {
            var gameSession = new GameSession(gameDescription);
            _gameSessions.Add(gameSession);
        }

        public byte[] GetInfo(int gameId)
        {
            var game = _gameSessions.Find(x => x.LocalGameDescription.GameId == gameId);

            return game.GetInfo();
        }

        /// <summary>
        /// Ищем игру, в которой находится игрок и удаляем его оттуда.
        /// </summary>
        public bool LeaveGame(SeaBattleService player)
        {
            try
            {
                player.LeaveGame();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] IsGameStarted(int gameId)
        {
            var game = _gameSessions.Find(x => x.LocalGameDescription.GameId == gameId);
            if (game == null || !game.LocalGameDescription.IsGameStarted)
                return null;
            return game.GetInfo();
        }

        public void HandleGameEvent(GameEvent gameEvent, string playerName, int gameId)
        {
            var game = _gameSessions.Find(x => x.LocalGameDescription.GameId == gameId);
            if (game == null || !game.LocalGameDescription.IsGameStarted)
                return;
            
            game.HandleGameEvent(gameEvent, playerName);
        }
    }
}
