using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ESDnevnik2023A
{
    internal class konekcija
    {
        public static SqlConnection vrati_vezu()
        {
            string CS;
            CS = ConfigurationManager.ConnectionStrings["skola"].ConnectionString;
            return new SqlConnection(CS);
        }
    }
}
