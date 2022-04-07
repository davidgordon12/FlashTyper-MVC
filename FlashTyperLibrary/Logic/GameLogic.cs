using System;
using System.Collections.Generic;
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
    }
}
