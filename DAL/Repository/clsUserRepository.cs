using System.Data;
using Microsoft.Data.SqlClient;
using DryCleanShopApi.DAL.Models;
using DryCleanShopApi.DAL;

namespace DryCleanShopApi.DAL.Repositories
{
    public class UserRepository
    {
        static readonly string _connectionString = DatabaseConnection.ConnectionString();

        // إضافة مستخدم جديد
        public static int AddUser(clsUser user)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("AddUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@CreatedBy", user.CreatedBy);

                var outputId = new SqlParameter("@NewUserID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)outputId.Value;
            }
        }

        // تعديل بيانات مستخدم
        public static bool UpdateUser(clsUser user, int updatedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_UpdateUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                var isUpdatedParam = new SqlParameter("@IsUpdated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isUpdatedParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)isUpdatedParam.Value; // true لو اتعدل، false لو مش موجود
            }
        }

        // حذف مستخدم (Soft Delete)
        public static bool DeleteUser(int userId, int deletedBy)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_DeleteUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@DeletedBy", deletedBy);

                var isDeletedParam = new SqlParameter("@IsDeleted", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(isDeletedParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)isDeletedParam.Value; // true لو اتحذف، false لو مش موجود
            }
        }

        // الحصول على كل المستخدمين (غير محذوفين)
        public static List<clsUser> GetAllUsers()
        {
            var users = new List<clsUser>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetAllUsers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new clsUser
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUser")),
                        };

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        // الحصول على مستخدم واحد باستخدام UserID
        public static clsUser? GetUserByID(int userId)
        {
            clsUser? user = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_GetUserByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new clsUser
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUser")),
                        };
                    }
                }
            }

            return user;
        }

        // التحقق إذا كان اسم المستخدم موجود بالفعل
        public static bool CheckUsernameExists(string username)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_CheckUsernameExists", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);

                var existsParam = new SqlParameter("@IsFound", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(existsParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (bool)existsParam.Value;
            }
        }

        // تسجيل الدخول
        public static clsUser? LoginUser(string username, string passwordHash)
        {
            clsUser? user = null;

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SP_LoginUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new clsUser
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            CreatedByUsername = reader.GetString(reader.GetOrdinal("CreatedByUser")),
                        };
                    }
                }
            }

            return user;
        }
    }
}



