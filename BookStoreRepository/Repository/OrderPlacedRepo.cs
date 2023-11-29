using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookStoreCommon.OrderPlaced;
using Utility;

namespace BookStoreRepository.Repository
{
    public class OrderPlacedRepo : IOrderPlacedRepo
    {
        private readonly IConfiguration iconfiguration;
        public OrderPlacedRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        Nlog nlog = new Nlog();
        public async Task<int> PlaceOrder(int CartId, int CustomerId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spPlaceOrder", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", CustomerId);
                com.Parameters.AddWithValue("@CartId", CartId);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Order Placed");
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
