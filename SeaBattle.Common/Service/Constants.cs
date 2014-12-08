using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.Service
{
    class Constants
    {
        private const int EventTimerDelayTime = 25;
        private const int SynchroFrameDelayTime = 500;
    }

    public enum AccountManagerErrorCode
    {
        Ok,
        UnknownExceptionOccured,
        InvalidUsernameOrPassword,
        UsernameTaken,
        UserIsAlreadyOnline,
        UserIsAlreadyOffline,
        UnknownError //if this value is returned, then AccountManager code must be bugged
    }
}
