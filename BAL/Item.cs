using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALItem
    {
        public static int AddItem(clsItem item)
        {
            return ItemRepository.AddItem(item);
        }

        public static List<clsItem> GetAllItems()
        {
            return ItemRepository.GetAllItems();
        }

        public static bool IsItemNameExists(string itemName)
        {
            return ItemRepository.IsItemNameExists(itemName);
        }
    }
}
