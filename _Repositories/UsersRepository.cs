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
        public class UsersRepository : BaseRepository, IUsersRepository
        {
            //Constructor
            public UsersRepository(string connectionString)
            {
                this.connectionString = connectionString;
            }
            //Methods
            public void Add(UsersModel usersModel)
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "insert into Users values (@name, @password, @role)";
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = usersModel.Name;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = usersModel.Password;
                    command.Parameters.Add("@role", SqlDbType.NVarChar).Value = usersModel.Role;
                    command.ExecuteNonQuery();
                }
            }
            public void Delete(int id)
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "delete from Users where Users_Id=@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.ExecuteNonQuery();
                }
            }
            public void Edit(UsersModel usersModel)
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = @"update Users 
                                        set Users_Name=@name,Users_Password= @password,Users_Role= @role
                                        where Users_Id=@id";
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = usersModel.Name;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = usersModel.Password;
                    command.Parameters.Add("@role", SqlDbType.NVarChar).Value = usersModel.Role;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = usersModel.Id;
                    command.ExecuteNonQuery();
                }
            }

            public IEnumerable<UsersModel> GetAll()
            {
                var usersList = new List<UsersModel>();
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
                            var usersModel = new UsersModel();
                        usersModel.Id = (int)reader[0];
                        usersModel.Name = reader[1].ToString();
                        usersModel.Password = reader[2].ToString();
                        usersModel.Role = reader[3].ToString();
                        usersList.Add(usersModel);
                        }
                    }
                }
                return usersList;
            }

            public IEnumerable<UsersModel> GetByValue(string value)
            {
                var usersList = new List<UsersModel>();
                int usersId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
                string usersName = value;
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = @"Select *from Users
                                        where Users_Id=@id or Users_Name like @name+'%' 
                                        order by Users_Id desc";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = usersId;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = usersName;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usersModel = new UsersModel();
                        usersModel.Id = (int)reader[0];
                        usersModel.Name = reader[1].ToString();
                        usersModel.Password = reader[2].ToString();
                        usersModel.Role = reader[3].ToString();
                        usersList.Add(usersModel);
                        }
                    }
                }
                return usersList;
            }
        }
    }
