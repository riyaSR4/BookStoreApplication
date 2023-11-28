using BookStoreCommon.Cart;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookStoreCommon.Feedback;

namespace BookStoreRepository.Repository
{
    public class FeedbackRepo : IFeedbackRepo
    {
        private readonly IConfiguration iconfiguration;
        public FeedbackRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddFeedback(Feedbacks feedback, int userId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddFeedback", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", feedback.BookId);
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@CustomerDescription", feedback.CustomerDescription);
                com.Parameters.AddWithValue("@Rating", feedback.Rating);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
