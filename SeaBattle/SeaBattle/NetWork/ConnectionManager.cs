using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using SeaBattle.Common.GameEvents;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;

namespace SeaBattle.NetWork
{
    internal class ConnectionManager : ISeaBattleService
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

        #region InitConnection

        private void InitializeConnection()
        {
            try
            {
                
            }
            catch (Exception e)
            {
                FatalError(e);
            }
        }

        private void FatalError(Exception e)
        {
            Trace.WriteLine(e);
        }

        #endregion

        #region регистрация, логин и создание игры

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

        public GameDescription[] GetGameList()
        {
            throw new NotImplementedException();
        }

        public GameDescription CreateGame(int maxPlayers, int teams)
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

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void ChangeAmmunition()
        {
            throw new NotImplementedException();
        }

        public AGameEvent[] GetEvents()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
