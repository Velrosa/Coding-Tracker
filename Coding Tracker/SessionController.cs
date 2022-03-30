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
        private static string conString = ConfigurationManager.AppSettings.Get("conString");

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
                                    EndTime = reader.GetString(2),
                                    Duration = reader.GetString(3)
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No Rows to Display.");
                        }
                    }
                }
            }
            return tableData;
        }

        public static void InsertRow(Session session)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO sessions (start_time, end_time, total_time) VALUES (@start_time, @end_time, @duration)";
                    cmd.Parameters.AddWithValue("@start_time", session.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", session.EndTime);
                    cmd.Parameters.AddWithValue("@duration", session.Duration);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateRow(Session session)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE sessions SET start_time=(@start_time), end_time=(@end_time), total_time=(@duration) WHERE id=(@id) ";
                    cmd.Parameters.AddWithValue("@id", session.Id);
                    cmd.Parameters.AddWithValue("@start_time", session.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", session.EndTime);
                    cmd.Parameters.AddWithValue("@duration", session.Duration);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRow(Session session)
        {
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "DELETE FROM sessions WHERE id=(@Id)";
                    cmd.Parameters.AddWithValue("@Id", session.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Session GetActive()
        {
            Session session = new Session();
            
            using (var con = new SQLiteConnection(conString))
            {
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT * FROM sessions WHERE end_time=(@endTime)";
                    cmd.Parameters.AddWithValue("@endTime", "Session Open.");

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                session.Id = reader.GetInt32(0);
                                session.StartTime = reader.GetString(1);
                                session.EndTime = reader.GetString(2);
                                session.Duration = reader.GetString(3);
                            }
                            return session;
                        }
                        else { return null; }

                    }
                }
            }
        }
    }
}
