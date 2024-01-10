using DataLibrary.DataAccess;
using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(string username, string password)
        {
            User data = new User();
            data.UserId = 0; // Generate UUID later.
            data.Username = username;
            data.Password = password;

            string sql = "INSERT INTO dbo.[User] (UserId, Username, Password) VALUES (@UserId, @Username, @Password)";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<User> LoadUsers()
        {
            string sql = @"select UserId, Username, Password from dbo.User";

            return SqlDataAccess.LoadData<User>(sql);
        }
    }
}
