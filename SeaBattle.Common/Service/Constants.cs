namespace SeaBattle.Common.Service
{
    class Constants
    {
        private const int EventTimerDelayTime = 25;
        private const int SynchroFrameDelayTime = 500;
    }

    public enum BulletType
    {
        Noone = 0,
        Cannonball = 1, // Ядра
        Buckshot = 2, // Картечь
        Barshots = 4, // Книппеля
        Bomb = 8 // Бомбы
    }

    public enum Side
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 3
    }

    public enum AccountManagerErrorCode
    {
        Ok,
        UnknownExceptionOccured,
        InvalidUsernameOrPassword,
        UsernameTaken,
        UserIsAlreadyOnline,
        UserIsAlreadyOffline,
        ServerUnavailable,
        UnknownError //if this value is returned, then AccountManager code must be bugged
    }
}
