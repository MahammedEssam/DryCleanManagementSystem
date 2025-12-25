namespace DryCleanShopApi.DAL.Models
{
    public class clsUser
    {
        public int? UserID { get; set; }              // رقم المستخدم
        public string Username { get; set; } = default!;   // اسم الدخول
        public string Name { get; set; } = default!;       // الاسم الحقيقي
        public string Phone { get; set; } = default!;      // رقم الهاتف
        public string PasswordHash { get; set; } = default!; // كلمة المرور مشفرة
        public int? Role { get; set; }                // رقم الدور (Admin, Worker, ...)
        public DateTime CreatedAt { get; set; }      // تاريخ الإنشاء
        public int? CreatedBy { get; set; }           // المستخدم اللي أنشأ الحساب

        // خصائص إضافية للعرض
        public string? RoleName { get; set; }        // اسم الدور (من جدول Roles)
        public string? CreatedByUsername { get; set; } // اسم المستخدم اللي أنشأ الحساب
    }
}
