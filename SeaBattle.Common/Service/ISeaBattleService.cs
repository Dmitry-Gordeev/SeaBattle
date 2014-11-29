using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeaBattle.Common.Service;

namespace SeaBattle.Common.Interfaces
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

        #endregion
    }
}
