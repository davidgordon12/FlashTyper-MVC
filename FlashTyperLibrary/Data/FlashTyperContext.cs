using System.Data.SqlClient;

namespace FlashTyperLibrary.Data
{
    public class FlashTyperContext
    {
        public SqlConnection Cnn { get; set; }
        public string CnnString { get; set; }

        public FlashTyperContext()
        {
            CnnString = "YOUR STRING HERE";

            Cnn = new SqlConnection(CnnString);
        }
    }
}
