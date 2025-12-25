using System.Data;
using System.Threading.Tasks.Dataflow;
using DryCleanShopApi.DAL;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL.Repositories;
using Microsoft.Data.SqlClient;

namespace BAL
{
    public class BALUser
    {
        public static int AddUser(clsUser user)
        {
            return UserRepository.AddUser(user);
        }

        public static bool UpdateUser(clsUser user, int updatedBy)
        {
            return UserRepository.UpdateUser(user, updatedBy);
        }

        public static bool DeleteUser(int userId, int deletedBy)
        {
            return UserRepository.DeleteUser(userId, deletedBy);
        }

        public static List<clsUser> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        public static clsUser? GetUserByID(int userId)
        {
            return UserRepository.GetUserByID(userId);
        }

        public static bool CheckUsernameExists(string username)
        {
            return UserRepository.CheckUsernameExists(username);
        }

        public static clsUser? LoginUser(string username, string passwordHash)
        {
            return UserRepository.LoginUser(username, passwordHash);
        }
    }
}
