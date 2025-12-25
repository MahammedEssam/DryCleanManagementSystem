using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class OrderDetailRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة تفاصيل طلب جديد
        public static int AddOrderDetail(clsOrderDetail detail, int CreatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddOrderDetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", detail.OrderID);
                cmd.Parameters.AddWithValue("@ItemID", detail.ItemID);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                cmd.Parameters.AddWithValue("@ServiceID", detail.ServiceID);
                cmd.Parameters.AddWithValue("@PricePerItem", detail.PricePerItem);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                var outputId = new SqlParameter("@NewDetailID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        // تعديل تفاصيل الطلب باستخدام DetailID
        public static bool UpdateOrderDetail(clsOrderDetail detail, int updatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateOrderDetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderDetailID", detail.DetailID);
                cmd.Parameters.AddWithValue("@ItemID", detail.ItemID);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                cmd.Parameters.AddWithValue("@ServiceID", detail.ServiceID);
                cmd.Parameters.AddWithValue("@PricePerItem", detail.PricePerItem);
                cmd.Parameters.AddWithValue("@CreatedBy", updatedBy);

                var isUpdatedParam = new SqlParameter("@IsUpdated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isUpdatedParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)isUpdatedParam.Value; // true لو اتعدل، false لو مش موجود
            }
        }

        // حذف تفاصيل الطلب باستخدام DetailID
        public static bool DeleteOrderDetail(int detailId, int deletedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_DeleteOrderDetail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderDetailID", detailId);
                cmd.Parameters.AddWithValue("@DeletedBy", deletedBy);

                var isDeletedParam = new SqlParameter("@IsDeleted", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isDeletedParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)isDeletedParam.Value; // true لو اتحذف، false لو مش موجود
            }
        }

        // الحصول على تفاصيل الطلب باستخدام OrderID
        public static List<clsOrderDetail> GetDetailsByOrderID(int orderId)
        {
            var details = new List<clsOrderDetail>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetOrderDetailsByOrderID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var detail = new clsOrderDetail
                        {
                            DetailID = reader.GetInt32(reader.GetOrdinal("DetailID")),
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            ItemName = reader["ItemName"] as string,
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ServiceName = reader["ServiceName"] as string,
                            PricePerItem = reader.GetDecimal(reader.GetOrdinal("PricePerItem")),
                            SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                        };

                        details.Add(detail);
                    }
                }
            }

            return details;
        }

        // الحصول على تفاصيل سطر واحد باستخدام DetailID
        public static clsOrderDetail? GetDetailByID(int detailId)
        {
            clsOrderDetail? detail = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetOrderDetailByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderDetailID", detailId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        detail = new clsOrderDetail
                        {
                            DetailID = reader.GetInt32(reader.GetOrdinal("DetailID")),
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            ItemName = reader["ItemName"] as string,
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ServiceName = reader["ServiceName"] as string,
                            PricePerItem = reader.GetDecimal(reader.GetOrdinal("PricePerItem")),
                            SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                        };
                    }
                }
            }

            return detail;
        }

        // إرجاع مجموع SubTotal للطلب
        public static decimal GetOrderTotalByOrderID(int orderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetOrderTotalByOrderID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                var totalParam = new SqlParameter("@Total", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 0,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(totalParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return totalParam.Value == DBNull.Value ? 0 : (decimal)totalParam.Value;
            }
        }
    }
}

