﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;

namespace SeaBattle.Service
{
    public class SeaBattleService : ISeaBattleService
    {
        #region private fields

        private static readonly List<SeaBattleService> ClientsList = new List<SeaBattleService>();

        private static int _globalID;

        private int _localID;

        private readonly InstanceContext _channelContext;

        private readonly Queue<AGameEvent> _filteredEvents = new Queue<AGameEvent>();

        #endregion

        public SeaBattleService()
		{
			_channelContext = OperationContext.Current.InstanceContext;
			_channelContext.Faulted += OnChannelStopped;
			_channelContext.Closed += OnChannelStopped;
			_localID = _globalID;
			_globalID++;
		}

        #region регистрация, аутентификация

        public AccountManagerErrorCode Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Guid? Login(string username, string password, out AccountManagerErrorCode errorCode)
        {
            throw new NotImplementedException();
        }

        public AccountManagerErrorCode Logout()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Основные методы инициализации игры

        public GameDescription[] GetGameList()
        {
            throw new NotImplementedException();
        }

        public GameDescription CreateGame(GameMode mode, int maxPlayers, int teams)
        {
            throw new NotImplementedException();
        }

        public bool JoinGame(GameDescription game)
        {
            throw new NotImplementedException();
        }

        public void LeaveGame()
        {
            throw new NotImplementedException();
        }

        public GameLevel GameStart(int gameId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region процесс игры

        public long GetServerGameTime()
        {
            throw new NotImplementedException();
        }

        public string[] PlayerListUpdate()
        {
            throw new NotImplementedException();
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
            LeaveGame();
            Logout();
        }

        #endregion
    }
}