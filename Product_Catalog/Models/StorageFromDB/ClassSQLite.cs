using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ClassCatalog;

namespace ClassCatalog
{
    class Sqlite
    {
        private static string connectionString = "Data sours=myCatalogDB.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void ExecuteSql (string sql, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
