namespace DryCleanShopApi.DAL.Models
{
    public class clsServicePrice
    {
        public int? ServicePriceID { get; set; }
        public int ItemID { get; set; }             // رقم القطعة
        public int ServiceID { get; set; }          // رقم الخدمة
        public decimal Price { get; set; }          // السعر
    }
}
