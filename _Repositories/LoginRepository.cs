using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using HotelsBooking.Models;

namespace HotelsBooking._Repositories
{
    class LoginRepository : BaseRepository, Models.ILoginRepository
    {
        //Constructor
        public LoginRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<LoginModel> GetAll()
        {
            var loginList = new List<LoginModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from Users order by Users_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var loginModel = new LoginModel();
                        loginModel.Name = reader[1].ToString();
                        loginModel.Password = reader[2].ToString();
                        loginList.Add(loginModel);
                    }
                }
            }
            return loginList;
        }
        public IEnumerable<LoginModel> GetByValue(string value)
        {
            var loginList = new List<LoginModel>();
            int loginId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string loginName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Users
                                        where   Users_Id=@id or Users_Name like @name+'%' 
                                        order by Users_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = usersId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = usersName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var loginModel = new LoginModel();
                        loginModel.Id = (int)reader[0];
                        loginModel.Name = reader[1].ToString();
                        loginList.Add(loginModel);
                    }
                }
            }
            return loginList;
        }
    }
}