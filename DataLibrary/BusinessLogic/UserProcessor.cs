using DataLibrary.DataAccess;
using DataLibrary.Models;
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
            Models.User data = new Models.User();
            data.Id = Guid.NewGuid().ToString();
            data.UserName = username;
            data.PasswordHash = password;

            string sql = "INSERT INTO dbo.[User] (Id, Username, Password) VALUES (@Id, @Username, @Password)";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<Models.User> LoadUsers()
        {
            string sql = @"SELECT Id, Username, Password from dbo.[User]";

            return SqlDataAccess.LoadData<Models.User>(sql);
        }
    }
}
