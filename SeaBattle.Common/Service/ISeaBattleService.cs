using System;
using System.ServiceModel;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Session;

namespace SeaBattle.Common.Service
{
    [ServiceContract]
    public interface ISeaBattleService
    {
        #region регистрация, аутентификация

        [OperationContract(IsInitiating = true)]
        AccountManagerErrorCode Register(string username, string password);

        [OperationContract(IsInitiating = true)]
        Guid? Login(string username, string password, out AccountManagerErrorCode errorCode);

        [OperationContract]
        AccountManagerErrorCode Logout();

        #endregion

        #region Основные методы инициализации игры

        [OperationContract]
        GameDescription[] GetGameList();

        [OperationContract]
        GameDescription CreateGame(GameMode mode, int maxPlayers, int teams);

        [OperationContract]
        bool JoinGame(GameDescription game);

        [OperationContract]
        void LeaveGame(int x, int y);

        /// <summary>
        /// проверка началась ли игра
        /// </summary>
        /// <param name="gameId">идентификатор игры</param>
        /// <returns>если игра не началась возвращает null</returns>
        [OperationContract]
        GameLevel GameStart(int gameId);

        #endregion
        
        #region процесс игры

        [OperationContract]
        long GetServerGameTime();

        /// <summary>
        /// возвращает список игроков
        /// </summary>
        [OperationContract]
        IPlayer[] PlayerListUpdate();

        [OperationContract]
        AGameEvent[] GetEvents();

        #endregion
    }
}
