using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALInvoice
    {
        public static int AddInvoice(clsInvoice invoice, int CreatedBy)
        {
            return InvoiceRepository.AddInvoice(invoice, CreatedBy);
        }

        public static clsInvoice? GetInvoiceByID(int invoiceId)
        {
            return InvoiceRepository.GetInvoiceByID(invoiceId);
        }

        public static decimal GetTotalPaidByDay(DateTime targetDate)
        {
            return InvoiceRepository.GetTotalPaidByDay(targetDate);
        }
    }
}
