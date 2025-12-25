using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALOrder
    {
        public static int AddOrder(clsOrder order)
        {
            return OrderRepository.AddOrder(order);
        }

        public static bool UpdateOrderStatus(int orderId, int status, int updatedBy)
        {
            return OrderRepository.UpdateOrderStatus(orderId, status, updatedBy);
        }

        public static bool UpdateOrderAmount(int orderId, decimal totalAmount, int updatedBy)
        {
            return OrderRepository.UpdateOrderAmount(orderId, totalAmount, updatedBy);
        }

        public static clsOrder? GetOrderByID(int orderId)
        {
            return OrderRepository.GetOrderByID(orderId);
        }

        public static List<clsOrder> GetAllOrders()
        {
            return OrderRepository.GetAllOrders();
        }

        public static List<clsOrder> GetOrdersByCustomerID(int customerId)
        {
            return OrderRepository.GetOrdersByCustomerID(customerId);
        }
    }
}
