namespace DryCleanShopApi.DAL.Models
{
    public class clsCustomer
    {
        public int CustomerID { get; set; }              // رقم العميل
        public string Name { get; set; } = default!;                // اسم العميل
        public string? Phone { get; set; }               // رقم الهاتف (nullable)
        public string? Address { get; set; }             // العنوان (nullable)
        public DateTime CreatedAt { get; set; }          // تاريخ الإنشاء
        public string CreatedBy { get; set; } = default!;
    }
}
