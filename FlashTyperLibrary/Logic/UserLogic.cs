using FlashTyperLibrary.Data;
using System.Data.SqlClient;
using FlashTyperLibrary.Model;
using System.Collections.Generic;
using System;

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
            try
            {
                FlashTyperContext context = new();
                SqlDataAdapter adapter = new();

                string _password = Hash.HashPassword(password);

                context.Cnn.Open();

                SqlCommand command = new()
                {
                    Connection = context.Cnn,
                    CommandText= $"INSERT INTO FlashTyperUsers (username, password) VALUES ('{username}', '{_password}')"
                };

                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                context.Cnn.Close();

                return true;
            }
            catch (SqlException)
            {
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

            using (var ctx = context.Cnn)
            {
                ctx.Open();

                SqlCommand command = new()
                {
                    Connection = context.Cnn,
                    CommandText = $"SELECT TOP 5 username, wpm, accuracy FROM FlashTyperUsers ORDER BY wpm DESC;"
                };

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    users.Add(new UserModel
                    {
                        Username = dataReader.GetString(0),
                        WPM = dataReader.GetInt32(1),
                        Accuracy = dataReader.GetDouble(2)
                    });
                }
            }


            return users;
        }

        public static bool ValidLogin(string username, string password)
        {
            try
            {
                FlashTyperContext context = new();
                SqlDataReader dataReader;

                string _password = Hash.HashPassword(password);

                context.Cnn.Open();

                SqlCommand command = new($"SELECT username, password FROM FlashTyperUsers WHERE username = '{username}' AND password = '{_password}';", context.Cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
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
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static UserModel GetStats(string username)
        {
            try
            {
                int wpm = 0;
                double accuracy = 0;

                FlashTyperContext context = new();
                SqlDataReader dataReader;

                context.Cnn.Open();

                SqlCommand command = new($"SELECT wpm, accuracy FROM FlashTyperUsers WHERE username = '{username}';", context.Cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    wpm = dataReader.GetInt32(0);
                    accuracy = dataReader.GetDouble(1);
                }

                command.Dispose();
                context.Cnn.Close();

                return new UserModel
                {
                    Username = username,
                    WPM = wpm,
                    Accuracy = accuracy
                };
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}