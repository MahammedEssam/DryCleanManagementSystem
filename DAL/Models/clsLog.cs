namespace DryCleanShopApi.DAL.Models
{
    public class clsLog
    {
        public int LogID { get; set; }                // رقم اللوج
        public string Action { get; set; } = default!; // نوع العملية (Add, Update, Delete...)
        public string TableName { get; set; } = default!; // اسم الجدول
        public int RecordID { get; set; }             // رقم السجل المتأثر
        public string UserName { get; set; } = default!;               // المستخدم اللي عمل العملية
        public DateTime CreatedAt { get; set; }       // وقت العملية
    }
}
