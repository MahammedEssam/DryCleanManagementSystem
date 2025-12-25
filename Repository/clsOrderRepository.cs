using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class OrderRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة طلب جديد وإرجاع رقم الطلب
        public static int AddOrder(clsOrder order)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddOrder", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                cmd.Parameters.AddWithValue("@CreatedBy", order.CreatedBy);
                cmd.Parameters.AddWithValue("@StatusID", order.Status);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount ?? (object)DBNull.Value);

                var outputId = new SqlParameter("@NewOrderID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        // تعديل حالة الطلب باستخدام ID
        public static bool UpdateOrderStatus(int orderId, int status, int updatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateOrderStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@NewStatusID", status);
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

        // تعديل إجمالي المبلغ باستخدام ID
        public static bool UpdateOrderAmount(int orderId, decimal totalAmount, int updatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateOrderTotalAmount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@NewTotalAmount", totalAmount);
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

        // الحصول على تفاصيل طلب باستخدام ID
        public static clsOrder? GetOrderByID(int orderId)
        {
            clsOrder? order = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetOrderByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new clsOrder
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUser")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            StatusName = reader.GetString(reader.GetOrdinal("StatusName")),
                            TotalAmount = reader["TotalAmount"] == DBNull.Value ? null : (decimal?)reader["TotalAmount"]
                        };
                    }
                }
            }

            return order;
        }

        // الحصول على كل الطلبات
        public static List<clsOrder> GetAllOrders()
        {
            var orders = new List<clsOrder>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetAllOrders", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new clsOrder
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerName = reader.GetString(reader.GetOrdinal("Name")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("Username")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            StatusName = reader.GetString(reader.GetOrdinal("Statue")),
                            TotalAmount = reader["TotalAmount"] == DBNull.Value ? null : (decimal?)reader["TotalAmount"]
                        };

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        // الحصول على الطلبات الخاصة بعميل معين
        public static List<clsOrder> GetOrdersByCustomerID(int customerId)
        {
            var orders = new List<clsOrder>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetOrdersByCustomerNotDelivered", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new clsOrder
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUser")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            StatusName = reader.GetString(reader.GetOrdinal("StatusName")),
                            TotalAmount = reader["TotalAmount"] == DBNull.Value ? null : (decimal?)reader["TotalAmount"]
                        };

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }
    }
}



