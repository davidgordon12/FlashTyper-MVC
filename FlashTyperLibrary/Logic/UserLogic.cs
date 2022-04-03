using FlashTyperLibrary.Data;
using System.Data.SqlClient;
using FlashTyperLibrary.Model;
using System.Collections.Generic;

namespace FlashTyperLibrary.Logic
{
    public class UserLogic
    {
        /// <summary>
        /// Inserts the user into the database with the given username and password from the account creation form
        /// </summary>
        /// <param name="username">username to be inserted</param>
        /// <param name="password">password to be hashed, then inserted</param>
        public static bool AddUser(string username, string password)
        {
            FlashTyperContext context = new();
            SqlDataAdapter adapter = new();

            string _password = Hash.HashPassword(password);

            context.cnn.Open();

            try
            {
                SqlCommand command = new($"INSERT INTO FlashTyperUsers (username, password) VALUES ('{username}', '{_password}')", context.cnn);

                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                context.cnn.Close();

                return true;
            }
            catch (SqlException)
            {
                context.cnn.Close();

                return false;
            }
        }

        /// <summary>
        /// Runs an SQL query that selects 5 users from the database with the highest WPM
        /// </summary>
        /// <returns>A list of the top 5 users in descending order</returns>
        public static List<UserModel> GetLeaderboard()
        {
            List<UserModel> users = new();

            FlashTyperContext context = new();
            SqlDataReader dataReader;

            context.cnn.Open();

            SqlCommand command = new($"SELECT TOP 5 username, wpm FROM FlashTyperUsers ORDER BY wpm DESC;", context.cnn);

            dataReader = command.ExecuteReader();

            while(dataReader.Read())
            {
                users.Add(new UserModel
                {
                    Username = dataReader.GetString(0),
                    WPM = dataReader.GetInt32(1)
                });
            }

            command.Dispose();
            context.cnn.Close();

            return users;
        }

        public static bool ValidLogin(string username, string password)
        {
            FlashTyperContext context = new();
            SqlDataReader dataReader;

            string _password = Hash.HashPassword(password);

            context.cnn.Open();

            SqlCommand command = new($"SELECT username, password FROM FlashTyperUsers WHERE username = '{username}' AND password = '{_password}';", context.cnn);

            dataReader = command.ExecuteReader();

            while(dataReader.Read())
            {
                if (dataReader.GetString(0) == username && dataReader.GetString(1) == _password)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
            
            command.Dispose();
            context.cnn.Close();

            return false;
        }
    }
}
