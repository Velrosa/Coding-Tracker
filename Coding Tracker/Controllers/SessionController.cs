using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace Coding_Tracker
{
    internal class SessionController
    {
        static string conString = ConfigurationManager.AppSettings.Get("conString");
        
        public static List<Session> GetTable()
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
            return tableData;
        }

        public static void InsertRow(string startTime, string endTime)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO sessions (start_time, end_time) VALUES (@start_time, @end_time)";
                    cmd.Parameters.AddWithValue("@start_time", startTime);
                    cmd.Parameters.AddWithValue("@end_time", endTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateRow(string entryId, string startTime, string endTime)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE sessions SET start_time=(@start_time), end_time=(@end_time) WHERE id=(@id) ";
                    cmd.Parameters.AddWithValue("@id", entryId);
                    cmd.Parameters.AddWithValue("@start_time", startTime);
                    cmd.Parameters.AddWithValue("@end_time", endTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRow(string entryId)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "DELETE FROM sessions WHERE id=(@Id)";
                    cmd.Parameters.AddWithValue("@Id", entryId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }   
        
}
