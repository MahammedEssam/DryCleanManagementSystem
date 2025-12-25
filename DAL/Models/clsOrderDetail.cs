namespace DryCleanShopApi.DAL.Models
{
    public class clsOrderDetail
    {
        public int DetailID { get; set; }             // رقم السطر
        public int OrderID { get; set; }              // رقم الطلب
        public int? ItemID { get; set; }               // رقم القطعة
        public int Quantity { get; set; }             // الكمية
        public int? ServiceID { get; set; }            // رقم الخدمة
        public decimal PricePerItem { get; set; }     // سعر الوحدة
        public decimal? SubTotal { get; set; }         // السعر الكلي

        // خصائص إضافية للعرض
        public string? ServiceName { get; set; }      // اسم الخدمة
        public string? ItemName { get; set; }         // اسم القطعة
    }
}
