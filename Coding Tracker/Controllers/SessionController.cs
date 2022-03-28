using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using ConsoleTableExt;

namespace Coding_Tracker
{
    internal class SessionController
    {
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        
        public static void GetTable(string selector)
        {
            List<Session> tableData = new List<Session>();

            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM sessions";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Session
                                {
                                    Id = reader.GetInt32(0),
                                    StartTime = reader.GetString(1),
                                    EndTime = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
            }
            TableView.ShowTable(tableData, selector);
        }

        public static void InsertRow()
        {

        }
    }   
        
}
