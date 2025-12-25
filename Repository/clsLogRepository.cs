using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class LogRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة Log جديد
        public static async Task AddLog(clsLog log)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddLog", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", log.Action);
                cmd.Parameters.AddWithValue("@TableName", log.TableName);
                cmd.Parameters.AddWithValue("@RecordID", log.RecordID);
                cmd.Parameters.AddWithValue("@UserName", log.UserName);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // الحصول على كل اللوجات
        public static List<clsLog> GetAllLogs()
        {
            var logs = new List<clsLog>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetLogs", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new clsLog
                        {
                            LogID = reader.GetInt32(reader.GetOrdinal("LogID")),
                            Action = reader.GetString(reader.GetOrdinal("ActionType")),
                            TableName = reader.GetString(reader.GetOrdinal("TableName")),
                            RecordID = reader.GetInt32(reader.GetOrdinal("RecordID")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("ActionDate"))
                        });
                    }
                }
            }

            return logs;
        }
    }
}
