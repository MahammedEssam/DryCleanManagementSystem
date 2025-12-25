using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALCustomer
    {
        public static int AddCustomer(clsCustomer customer, int CreatedBy)
        {
            return CustomerRepository.AddCustomer(customer, CreatedBy);
        }

        public static bool UpdateCustomer(clsCustomer customer, int UpdatedBy)
        {
            return CustomerRepository.UpdateCustomer(customer, UpdatedBy);
        }

        public static List<clsCustomer> GetAllCustomers()
        {
            return CustomerRepository.GetAllCustomers();
        }

        public static clsCustomer? GetCustomerByID(int customerId)
        {
            return CustomerRepository.GetCustomerByID(customerId);
        }

        public static List<clsCustomer> GetCustomerByName(string name)
        {
            return CustomerRepository.GetCustomerByName(name);
        }
    }
}
