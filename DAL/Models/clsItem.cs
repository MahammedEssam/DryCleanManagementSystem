namespace DryCleanShopApi.DAL.Models
{
    public class clsItem
    {
        public int? ItemID { get; set; }              // رقم القطعة
        public string ItemName { get; set; } = "";   // اسم القطعة
        public DateTime CreatedAt { get; set; }      // تاريخ الإنشاء
        public int? CreatedBy { get; set; }           // رقم المستخدم اللي أنشأ القطعة

        // خصائص إضافية للعرض
        public string? CreatedByUsername { get; set; } // اسم المستخدم (اختياري للعرض)
    }
}
