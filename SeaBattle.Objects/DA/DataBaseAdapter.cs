using System;
using System.Data;
using System.Data.SqlClient;
using SeaBattle.Common.Service;

namespace SeaBattle.Service.DA
{
    public class DataBaseAdapter
    {
        #region Singleton

        private static DataBaseAdapter _instance;
        public static DataBaseAdapter Instance
        {
            get { return _instance ?? (_instance = new DataBaseAdapter()); }
        }
        
        #endregion

        private const string ConnectionString = "Server=localhost\\Gordeev;Database=SeaBattle;User Id=Sailor;Password=seapass;";

        public AccountManagerErrorCode RegisterPlayer(string login, string password)
        {
            bool result;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand("p_Register_Player", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                        connection.Open();
                        result = Convert.ToBoolean(cmd.ExecuteScalar());
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHelper.ErrorHelper.FatalError(e);
                return AccountManagerErrorCode.UnknownError;
            }

            return result ? AccountManagerErrorCode.Ok : AccountManagerErrorCode.UsernameTaken;
        }

        public AccountManagerErrorCode GetPlayerStatus(string login, string password)
        {
            bool result;
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand("p_Check_Player", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Login", SqlDbType.VarChar).Value = login;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                        connection.Open();
                        result = Convert.ToBoolean(cmd.ExecuteScalar());
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHelper.ErrorHelper.FatalError(e);
                return AccountManagerErrorCode.UnknownError;
            }

            return result ? AccountManagerErrorCode.Ok : AccountManagerErrorCode.InvalidUsernameOrPassword;
        }
    }
}
