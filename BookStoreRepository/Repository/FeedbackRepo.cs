﻿using BookStoreCommon.Cart;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookStoreCommon.Feedback;
using Utility;
using BookStoreCommon.Wishlist;
using BookStoreCommon.Book;

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
        Nlog nlog = new Nlog();
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
                nlog.LogDebug("Feedback Added");
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
        public IEnumerable<Feedbacks> GetAllFeedback(int UserId)
        {
            try
            {
                Connection();
                List<Feedbacks> feedbacks = new List<Feedbacks>();
                SqlCommand com = new SqlCommand("spGetAllFeedback", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    feedbacks.Add(
                       new Feedbacks()
                       {
                           FeedbackId = Convert.ToInt32(dr["FeedbackId"]),
                           UserId = Convert.ToInt32(dr["UserId"]),
                           BookId = Convert.ToInt32(dr["BookId"]),
                           CustomerDescription = Convert.ToString(dr["CustomerDescription"]),
                           Rating = Convert.ToInt32(dr["Rating"])
                       }
                       );
                }
                nlog.LogDebug("Got all Books");
                return feedbacks;
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
