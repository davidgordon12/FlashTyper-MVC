using FlashTyperLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashTyperLibrary.Logic
{
    public class GameLogic
    {
        public static int CalculateWPM(string input)
        {
            return (int)Math.Round(((input.Trim().Length / 5) / 0.33));
        }

        public static void UpdateWPM(string username, int wpm)
        {
            FlashTyperContext context = new();
            SqlDataReader dataReader;
            SqlDataAdapter adapter = new();
            SqlCommand command;
            int _wpm = 0;

            context.cnn.Open();

            // check if new WPM is greater than previous score

            command = new($"SELECT wpm FROM FlashTyperUsers WHERE username = '{username}'", context.cnn);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
               _wpm  = dataReader.GetInt32(0);
            }

            command.Dispose();
            dataReader.Close();

            if (_wpm < wpm)
            {
                command = new($"UPDATE FlashTyperUsers SET wpm = '{wpm}' WHERE username = '{username}'", context.cnn);

                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                context.cnn.Close();
            }
            else
            {
                command.Dispose();
                context.cnn.Close();
            }
        }
    }
}
