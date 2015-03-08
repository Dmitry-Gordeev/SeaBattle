using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Service.ErrorHelper;
using SeaBattle.Utils;

namespace SeaBattle.NetWork
{
    class ConnectionManager
    {
        private static ConnectionManager _localInstance;

        public static ConnectionManager Instanse
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
        private List<AGameEvent> _lastServerGameEvents;
        private Queue<AGameEvent> _lastClientGameEvents;

        private readonly object _gameEventLocker = new object();

        #endregion

        #region timers

        private readonly Timer _eventTimer;
        private readonly Timer _synchroFrameTimer;
        private readonly Timer _pingTimer;
        private ISeaBattleService _service;

        #endregion

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

            _eventTimer.Dispose();
            _synchroFrameTimer.Dispose();
            _pingTimer.Dispose();
        }

        #endregion

        #region getting the last synchroFrame and game events from server

        public void GetLatestServerGameEvents()
        {

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
        /// Возвращает последние события от сервера, которые были получены с помощью другого потока
        /// Используется клиентом
        /// </summary>
        public AGameEvent[] GetEvents()
        {
            AGameEvent[] events;
            lock (_gameEventLocker)
            {
                events = _lastServerGameEvents.ToArray();
                _lastServerGameEvents.Clear();
            }
            // Logger.PrintEvents(events);
            return events;
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
                return AccountManagerErrorCode.UnknownExceptionOccured;
            }
        }

        public Guid? Login(string username, string password, out AccountManagerErrorCode accountManagerErrorCode)
        {
            // initialize connection
            InitializeConnection();

            accountManagerErrorCode = AccountManagerErrorCode.Ok;

            Guid? login = null;
            try
            {
                login = _service.Login(username, HashHelper.GetMd5Hash(password), out accountManagerErrorCode);
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
            }

            return login;
        }

        public AccountManagerErrorCode Logout()
        {
            var errorCode = AccountManagerErrorCode.UnknownError;
            try
            {
                errorCode = _service.Logout();
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
            }

            return errorCode;
        }

        public GameDescription[] GetGameList()
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

        public GameDescription CreateGame(GameMode mode, int maxPlayers, int teams)
        {
            try
            {
                return _service.CreateGame(mode, maxPlayers, teams);
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
                return null;
            }
        }

        public bool JoinGame(GameDescription game)
        {
            try
            {
                return _service.JoinGame(game);
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
                return false;
            }
        }

        public void LeaveGame()
        {
            try
            {
                _service.LeaveGame(1,1);
            }
            catch (Exception e)
            {
                ErrorHelper.FatalError(e);
            }
        }

        #endregion
    }
}
