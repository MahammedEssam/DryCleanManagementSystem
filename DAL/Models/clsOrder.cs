namespace DryCleanShopApi.DAL.Models
{
    public class clsOrder
    {
        public int OrderID { get; set; }              // رقم الطلب
        public int? CustomerID { get; set; }           // رقم العميل
        public int? CreatedBy { get; set; }            // رقم المستخدم اللي أنشأ الطلب
        public DateTime OrderDate { get; set; }       // تاريخ الطلب
        public int? Status { get; set; }               // حالة الطلب (كود رقمي)
        public decimal? TotalAmount { get; set; }     // إجمالي المبلغ (nullable)

        // خصائص إضافية للعرض
        public string? CustomerName { get; set; }     // اسم العميل
        public string? CreatedByUsername { get; set; } // اسم المستخدم اللي أنشأ الطلب
        public string? StatusName { get; set; }       // وصف الحالة (مثلاً: جديد، جاهز، تم التسليم)
    }
}
