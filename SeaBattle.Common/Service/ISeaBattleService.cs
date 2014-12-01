using System;
using System.ServiceModel;
using SeaBattle.Common.GameEvents;
using SeaBattle.Common.Session;

namespace SeaBattle.Common.Service
{
    [ServiceContract]
    public interface ISeaBattleService
    {
        #region регистрация, логин и создание игры

        [OperationContract(IsInitiating = true)]
        AccountManagerErrorCode Register(string username, string password);

        [OperationContract(IsInitiating = true)]
        Guid? Login(string username, string password, out AccountManagerErrorCode errorCode);

        [OperationContract]
        AccountManagerErrorCode Logout();

        [OperationContract]
        GameDescription[] GetGameList();

        [OperationContract]
        GameDescription CreateGame(GameMode mode, int maxPlayers, int teams);

        [OperationContract]
        bool JoinGame(GameDescription game);

        [OperationContract]
        void LeaveGame();

        /// <summary>
        /// проверка началась ли игра
        /// </summary>
        /// <param name="gameId">идентификатор игры</param>
        /// <returns>если игра не началась возвращает null</returns>
        [OperationContract]
        GameLevel GameStart(int gameId);

        [OperationContract]
        long GetServerGameTime();

        /// <summary>
        /// возвращает список игроков
        /// </summary>
        /// <returns>массив имен игроков</returns>
        [OperationContract]
        String[] PlayerListUpdate();

        #endregion

        #region процесс игры

        [OperationContract]
        AGameEvent[] GetEvents();

        #endregion
    }
}
