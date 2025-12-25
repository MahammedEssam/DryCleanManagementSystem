using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class InvoiceRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة فاتورة جديدة وإرجاع رقمها
        public static int AddInvoice(clsInvoice invoice, int CreatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_AddInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", invoice.OrderID);
                cmd.Parameters.AddWithValue("@PaidAmount", invoice.PaidAmount);
                cmd.Parameters.AddWithValue("@PaymentDate", invoice.PaymentDate);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                // باراميتر للإخراج
                var outputId = new SqlParameter("@NewInvoiceID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value; // بيرجع رقم الفاتورة الجديد
            }
        }

        // الحصول على فاتورة باستخدام ID
        public static clsInvoice? GetInvoiceByID(int invoiceId)
        {
            clsInvoice? invoice = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetInvoiceWithDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        invoice = new clsInvoice()
                        {
                            InvoiceID = reader.GetInt32(reader.GetOrdinal("InvoiceID")),
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            PaidAmount = reader.GetDecimal(reader.GetOrdinal("PaidAmount")),
                            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                            CreatedBy = reader.GetString(reader.GetOrdinal("CreatedByUser"))
                        };
                    }
                }
            }

            return invoice;
        }

        // الحصول على إجمالي المدفوع في يوم معين
        public static decimal GetTotalPaidByDay(DateTime targetDate)
        {
            decimal totalPaid = 0;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetTotalPaidByDay", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetDate", targetDate.Date);

                conn.Open();
                var result = cmd.ExecuteScalar(); // بيرجع قيمة واحدة (TotalPaid)

                if (result != DBNull.Value)
                    totalPaid = Convert.ToDecimal(result);
            }

            return totalPaid;
        }
    }
}
