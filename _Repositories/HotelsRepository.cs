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
    class HotelsRepository : BaseRepository, Models.IHotelsRepository
    {
        //Constructor
        public HotelsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Methods
        public void Add(HotelsModel hotelsModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Hotels values (@name)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelsModel.Name;
                command.ExecuteNonQuery();
            }
        }

        //public void Rezervare(HotelsModel hotelsModel)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    using (var command = new SqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "insert into Rezervari values (@name)";
        //        command.Parameters.Rezervare("@name", SqlDbType.NVarChar).Value = hotelsModel.Name;
        //        command.ExecuteNonQuery();
        //    }
        //}

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Hotels where Hotels_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(HotelsModel hotelsModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Hotels 
                                        set Hotels_Name=@name
                                        where Hotels_Id=@id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelsModel.Name;
                command.Parameters.Add("@id", SqlDbType.Int).Value = hotelsModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<HotelsModel> GetAll()
        {
            var hotelsList = new List<HotelsModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from Hotels order by Hotels_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotelsModel = new HotelsModel();
                        hotelsModel.Id = (int)reader[0];
                        hotelsModel.Name = reader[1].ToString();
                        hotelsList.Add(hotelsModel);
                    }
                }
            }
            return hotelsList;
        }

        public IEnumerable<HotelsModel> GetByValue(string value)
        {
            var hotelsList = new List<HotelsModel>();
            int hotelsId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string hotelsName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Hotels
                                        where Hotels_Id=@id or Hotels_Name like @name+'%' 
                                        order by Hotels_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = hotelsId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = hotelsName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hotelsModel = new HotelsModel();
                        hotelsModel.Id = (int)reader[0];
                        hotelsModel.Name = reader[1].ToString();
                        hotelsList.Add(hotelsModel);
                    }
                }
            }
            return hotelsList;
        }
    }
}
