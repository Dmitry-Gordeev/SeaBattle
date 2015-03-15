using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
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
        private Queue<AGameEvent> _lastClientGameEvents;

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

        private void InitializeThreadAndTimers()
        {
            _lastClientGameEvents = new Queue<AGameEvent>();

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
                AGameEvent gameEvent = null;

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
        
        #region sending client game events

        private void AddClientGameEvent(AGameEvent gameEvent)
        {
            lock (_locker)
                _lastClientGameEvents.Enqueue(gameEvent);
            _queue.Set();
        }

        private void SendClientGameEvent(AGameEvent gameEvent)
        {

        }

        #endregion

        #region service implementation

        /// <summary>
        /// Возвращает последние данные от сервера, которые были получены с помощью другого потока
        /// Используется клиентом
        /// </summary>
        public byte[] GetInfo()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region other service methods

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

        public int CreateGame(GameMode mode, int maxPlayers)
        {
            try
            {
                return _service.CreateGame(mode, maxPlayers);
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
        
        public GameLevel GameStart(int gameId)
        {
            try
            {
                return _service.GameStart(gameId);
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
                return null;
            }
        }

        public long GetServerGameTime()
        {
            throw new NotImplementedException();
        }

        public List<string> PlayerListUpdate()
        {
            try
            {
                return _service.PlayerListUpdate();
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
                return null;
            };
        }

        public AGameEvent[] GetEvents()
        {
            throw new NotImplementedException();
        }

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
