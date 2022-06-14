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
    class RezervariRepository : BaseRepository, Models.IRezervariRepository
    {
        //Constructor
        public RezervariRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Methods
        //public void Add(RezervariModel rezervariModel)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    using (var command = new SqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "insert into Rezervari values (@name)";
        //        command.Parameters.Add("@name", SqlDbType.NVarChar).Value = rezervariModel.Name;
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
                command.CommandText = "delete from Rezervari where Rezervari_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<RezervariModel> GetAll()
        {
            var rezervariList = new List<RezervariModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select *from Rezervari order by Rezervari_Id desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rezervariModel = new RezervariModel();
                        rezervariModel.Id = (int)reader[0];
                        rezervariModel.Name = reader[1].ToString();
                        rezervariList.Add(rezervariModel);
                    }
                }
            }
            return rezervariList;
        }

        public IEnumerable<RezervariModel> GetByValue(string value)
        {
            var rezervariList = new List<RezervariModel>();
            int rezervariId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string rezervariName = value;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"Select *from Rezervari
                                        where Rezervari_Id=@id or Rezervari_Name like @name+'%' 
                                        order by Hotels_Id desc";
                command.Parameters.Add("@id", SqlDbType.Int).Value = rezervariId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = rezervariName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rezervariModel = new RezervariModel();
                        rezervariModel.Id = (int)reader[0];
                        rezervariModel.Name = reader[1].ToString();
                        rezervariList.Add(rezervariModel);
                    }
                }
            }
            return rezervariList;
        }
    }
}
