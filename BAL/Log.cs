using System.Data;
using System.Threading.Tasks.Dataflow;
using DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALLog
    {
        public static async Task AddLog(clsLog log)
        {
             await LogRepository.AddLog(log);
        }

        public static List<clsLog> GetAllLogs()
        {
            return LogRepository.GetAllLogs();
        }
    }
}
