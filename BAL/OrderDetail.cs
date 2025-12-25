using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALOrderDetail
    {
        public static int AddOrderDetail(clsOrderDetail detail, int CreatedBy)
        {
            int detailID = OrderDetailRepository.AddOrderDetail(detail, CreatedBy);
            BALOrder.UpdateOrderAmount(detail.OrderID, GetOrderTotalByOrderID(detail.OrderID), CreatedBy);
            return detailID;
        }

        public static bool UpdateOrderDetail(clsOrderDetail detail, int updatedBy)
        {
            bool isUpdated = OrderDetailRepository.UpdateOrderDetail(detail, updatedBy);
            if (isUpdated)
            {
                BALOrder.UpdateOrderAmount(detail.OrderID, GetOrderTotalByOrderID(detail.OrderID), updatedBy);
            }
            return isUpdated;
        }

        public static bool DeleteOrderDetail(int detailID, int deletedBy)
        {
            clsOrderDetail? detail = OrderDetailRepository.GetDetailByID(detailID);
            bool isDeleted = OrderDetailRepository.DeleteOrderDetail(detailID, deletedBy);
            if (isDeleted)
            {
                if (detail != null)
                {
                    BALOrder.UpdateOrderAmount(detail.OrderID, GetOrderTotalByOrderID(detail.OrderID), deletedBy);
                }
            }
            return isDeleted;
        }

        public static List<clsOrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            return OrderDetailRepository.GetDetailsByOrderID(orderID);
        }

        public static clsOrderDetail? GetOrderDetailByID(int detailID)
        {
            return OrderDetailRepository.GetDetailByID(detailID);
        }

        public static decimal GetOrderTotalByOrderID(int orderId)
        {
            return OrderDetailRepository.GetOrderTotalByOrderID(orderId);
        }
    }
}
