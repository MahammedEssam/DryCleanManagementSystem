namespace DryCleanShopApi.DAL.Models
{
    public class clsInvoice
    {
        public int InvoiceID { get; set; }              // رقم الفاتورة
        public int OrderID { get; set; }                // رقم الأوردر المرتبط
        public decimal PaidAmount { get; set; }         // المبلغ المدفوع
        public DateTime PaymentDate { get; set; }       // تاريخ الدفع
        public string CreatedBy { get; set; } = default!;      // المستخدم الذي أنشأ الفاتورة
    }
}
