using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class ServicePriceRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة سعر جديد وإرجاع رقم السطر الجديد
        public static void AddServicePrice(clsServicePrice price)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddServicePrice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemID", price.ItemID);
                cmd.Parameters.AddWithValue("@ServiceID", price.ServiceID);
                cmd.Parameters.AddWithValue("@Price", price.Price);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // تعديل سعر باستخدام ID
        public static bool UpdateServicePrice(clsServicePrice price, int CreatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateServicePrice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ServicePriceID", price.ServicePriceID);
                cmd.Parameters.AddWithValue("@Price", price.Price);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; // لو اتعدل فعلاً بيرجع true
            }
        }

        // الحصول على السعر بناءً على ItemID و ServiceID
        public static decimal GetServicePrice(int itemId, int serviceId)
        {
            decimal price = 0;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetServicePrice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemID", itemId);
                cmd.Parameters.AddWithValue("@ServiceID", serviceId);

                var outputPrice = new SqlParameter("@Price", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputPrice);

                conn.Open();
                cmd.ExecuteNonQuery();

                price = (decimal)outputPrice.Value;
            }

            return price;
        }
    }
}