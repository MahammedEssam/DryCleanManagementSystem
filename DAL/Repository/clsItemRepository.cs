using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class ItemRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة قطعة جديدة
        public static int AddItem(clsItem item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);

                var outputId = new SqlParameter("@NewItemID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        // الحصول على كل القطع
        public static List<clsItem> GetAllItems()
        {
            var items = new List<clsItem>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetAllItems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new clsItem
                        {
                            ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                            ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedByUsername = reader["CreatedByUser"] as string
                        };

                        items.Add(item);
                    }
                }
            }

            return items;
        }

        // التحقق إذا كان اسم القطعة موجود بالفعل
        public static bool IsItemNameExists(string itemName)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_CheckItemNameExists", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchName", itemName);

                var existsParam = new SqlParameter("@IsFound", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(existsParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)existsParam.Value;
            }
        }
    }
}


