using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
    }
}
