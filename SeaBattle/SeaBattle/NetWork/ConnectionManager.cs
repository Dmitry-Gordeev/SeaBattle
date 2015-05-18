using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Service.ErrorHelper;
using SeaBattle.Utils;

namespace SeaBattle.NetWork
{
    class ConnectionManager : ISeaBattleService
    {
        private static ConnectionManager _localInstance;

        public static ConnectionManager Instance
        {
            get { return _localInstance ?? (_localInstance = new ConnectionManager()); }
        }

        private ConnectionManager()
        {

        }

        #region local thread

        private readonly EventWaitHandle _queue = new AutoResetEvent(false);

        private readonly object _locker = new object();

        private Thread _thread;

        #endregion

        #region game events

        // received from server
        private Queue<GameEvent> _lastClientGameEvents;

        private readonly object _gameEventLocker = new object();

        #endregion

        private ISeaBattleService _service;

        private void InitializeConnection()
        {
            try
            {
                var channelFactory = new ChannelFactory<ISeaBattleService>("SeaBattleEndpoint");
                _service = channelFactory.CreateChannel();
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
            }
        }

        #region run/stop thread, initialization

        internal void InitializeThreadAndTimers()
        {
            _lastClientGameEvents = new Queue<GameEvent>();

            _thread = new Thread(Run)
            {
                Name = "ConnectionManager"
            };
            _thread.Start();
        }

        public void Run()
        {
            while (true)
            {
                GameEvent gameEvent = null;

                lock (_locker)
                {
                    if (_lastClientGameEvents.Count > 0)
                    {
                        gameEvent = _lastClientGameEvents.Dequeue();
                        if (gameEvent == null)
                            return;
                    }
                }

                if (gameEvent != null)
                    SendClientGameEvent(gameEvent);
                else
                    _queue.WaitOne();
            }
        }

        public void Stop()
        {
            // stopping thread
            AddClientGameEvent(null);
            _thread.Join();
        }

        public void Dispose()
        {
            // stopping thread
            AddClientGameEvent(null);
            _thread.Join();

            // close EventWaitHandle
            _queue.Close();
        }

        #endregion
        
        #region service implementation

            #region Получение информации с сервера во время сессии

            /// <summary>
            /// Возвращает последние данные от сервера, которые были получены с помощью другого потока
            /// Используется клиентом
            /// </summary>
            public byte[] GetInfo()
            {
                try
                {
                    return _service.GetInfo();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return null;
                }
            }

            #endregion

            #region Регистрация, авторизация

            public AccountManagerErrorCode Register(string username, string password)
            {
                // initialize connection
                InitializeConnection();

                try
                {
                    return _service.Register(username, HashHelper.GetMd5Hash(password));
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return AccountManagerErrorCode.UnknownError;
                }
            }

            public AccountManagerErrorCode Login(string username, string password)
            {
                // initialize connection
                InitializeConnection();
                var errorCode = AccountManagerErrorCode.Ok;

                try
                {
                    errorCode = _service.Login(username, HashHelper.GetMd5Hash(password));

                    if (errorCode == AccountManagerErrorCode.InvalidUsernameOrPassword)
                    {
                        _service.Logout();
                    }

                    return errorCode;
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    if (errorCode != AccountManagerErrorCode.InvalidUsernameOrPassword)
                    {
                        errorCode = AccountManagerErrorCode.UnknownExceptionOccured;
                    }
                }
                return errorCode;
            }

            public void Logout()
            {
                try
                {
                    _service.Logout();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                }
            }

            #endregion

            #region Ожидание начала игры

            public List<GameDescription> GetGameList()
            {
                try
                {
                    return _service.GetGameList();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return null;
                }
            }

            public int CreateGame(GameModes modes, int maxPlayers, MapSet mapType)
            {
                try
                {
                    return _service.CreateGame(modes, maxPlayers, mapType);
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return -1;
                }
            }

            public bool JoinGame(int gameId)
            {
                try
                {
                    return _service.JoinGame(gameId);
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return false;
                }
            }

            public byte[] IsGameStarted(int gameId)
            {
                try
                {
                    return _service.IsGameStarted(gameId);
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return null;
                }
            }

            public bool StartGameSession()
            {
                try
                {
                    return _service.StartGameSession();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return false;
                }
            }

            public List<Player> PlayerListUpdate()
            {
                try
                {
                    return _service.PlayerListUpdate();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return null;
                }
            }

            public bool IsHost()
            {
                try
                {
                    return _service.IsHost();
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                    return false;
                }
            }

            #endregion

            #region sending client game events

            public void AddClientGameEvent(GameEvent gameEvent)
            {
                lock (_locker)
                    _lastClientGameEvents.Enqueue(gameEvent);
                _queue.Set();
            }

            private void SendClientGameEvent(GameEvent gameEvent)
            {
                try
                {
                    _service.AddClientGameEvent(gameEvent);
                }
                catch (Exception e)
                {
                    ErrorHelper.FatalError(e);
                }
            }

            #endregion

        #endregion

        #region other service methods

        public void LeaveGame()
        {
            try
            {
                _service.LeaveGame();
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
            }
        }

        #endregion
    }
}
