using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class CustomerRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة عميل جديد وإرجاع رقم العميل
        public static int AddCustomer(clsCustomer customer, int CreatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddCustomer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                var outputId = new SqlParameter("@NewCustomerID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        // تعديل بيانات عميل باستخدام ID
        public static bool UpdateCustomer(clsCustomer customer, int UpdatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateCustomer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);

                var isUpdatedParam = new SqlParameter("@IsUpdated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isUpdatedParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)isUpdatedParam.Value;
            }
        }

        // الحصول على كل العملاء
        public static List<clsCustomer> GetAllCustomers()
        {
            var customers = new List<clsCustomer>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetAllCustomers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new clsCustomer
                        {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader["Phone"] as string,
                            Address = reader["Address"] as string,
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedByUsername"))
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        // الحصول على عميل باستخدام ID
        public static clsCustomer? GetCustomerByID(int customerId)
        {
            clsCustomer? customer = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetCustomerByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new clsCustomer
                        {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader["Phone"] as string,
                            Address = reader["Address"] as string,
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedByUsername"))
                        };
                    }
                }
            }

            return customer;
        }

        // الحصول على العملاء باستخدام الاسم
        public static List<clsCustomer> GetCustomerByName(string name)
        {
            var customers = new List<clsCustomer>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetCustomerByName", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new clsCustomer
                        {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader["Phone"] as string,
                            Address = reader["Address"] as string,
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedByUsername"))
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }
    }
}


