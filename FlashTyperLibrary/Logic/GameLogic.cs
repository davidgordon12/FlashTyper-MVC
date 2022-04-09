using FlashTyperLibrary.Data;
using System;
using System.Data.SqlClient;

namespace FlashTyperLibrary.Logic
{
    public class GameLogic
    {
        public static int CalculateWPM(string input)
        {
            if(input is null)
            {
                return 0;
            }

            return (int)Math.Round((input.Trim().Length / 5) / 0.33);
        }

        public static float CalculateAccuracy(string input, string words)
        {
            if (input is null)
            {
                return 0;
            }

            int correctWord = 0;

            string[] inputArray = input.Split(' ');
            string[] wordsArray = words.Split(' ');

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] == wordsArray[i])
                {
                    correctWord++;
                }
            }

            return MathF.Round(((float)correctWord / (float)inputArray.Length) * 100);
        }

        public static void UpdateStats(string username, int wpm, float acc)
        {
            // only submit scores with accuracy > 95%
            if(acc < 95)
            {
                return;
            }

            FlashTyperContext context = new();

            int _wpm = 0;

            using (var ctx = context.Cnn)
            {
                SqlDataReader dataReader;
                SqlDataAdapter adapter = new();
                SqlCommand command;

                ctx.Open();

                // check if new WPM is greater than previous score

                command = new($"SELECT wpm FROM FlashTyperUsers WHERE username = '{username}'", context.Cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    _wpm = dataReader.GetInt32(0);
                }

                command.Dispose();
                dataReader.Close();

                if (_wpm < wpm)
                {
                    command = new($"UPDATE FlashTyperUsers SET wpm = '{wpm}', accuracy = '{acc}' WHERE username = '{username}'", context.Cnn);

                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
