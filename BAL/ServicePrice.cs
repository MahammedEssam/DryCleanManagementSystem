using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALServicePrice
    {
        public static void AddServicePrice(clsServicePrice price)
        {
           ServicePriceRepository.AddServicePrice(price);
        }

        public static bool UpdateServicePrice(clsServicePrice price, int CreatedBy)
        {
            return ServicePriceRepository.UpdateServicePrice(price, CreatedBy);
        }

        public static decimal GetServicePrice(int itemId, int serviceId)
        {
            return ServicePriceRepository.GetServicePrice(itemId, serviceId);
        }
    }
}
