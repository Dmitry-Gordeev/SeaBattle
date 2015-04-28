using System.Collections.Generic;
using System.ServiceModel;
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
        AccountManagerErrorCode Login(string username, string password);

        [OperationContract]
        void Logout();

        #endregion

        #region Основные методы инициализации игры

        [OperationContract]
        List<GameDescription> GetGameList();

        [OperationContract]
        int CreateGame(GameModes modes, int maxPlayers, MapSet mapType);

        [OperationContract]
        bool JoinGame(int gameId);

        [OperationContract]
        bool IsHost();

        [OperationContract]
        void LeaveGame();

        /// <summary>
        /// проверка началась ли игра
        /// </summary>
        /// <param name="gameId">идентификатор игры</param>
        /// <returns>если игра не началась возвращает null</returns>
        [OperationContract]
        byte[] IsGameStarted(int gameId);

        [OperationContract]
        bool StartGameSession();

        #endregion
        
        #region процесс игры

        [OperationContract]
        long GetServerGameTime();

        [OperationContract]
        byte[] GetInfo();

        /// <summary>
        /// возвращает список игроков
        /// </summary>
        [OperationContract]
        List<string> PlayerListUpdate();

        #endregion
    }
}
