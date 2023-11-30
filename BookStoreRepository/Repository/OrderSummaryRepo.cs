using BookStoreCommon.Cart;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Utility;
using BookStoreCommon.OrderSummary;
using BookStoreCommon.Book;
using BookStoreCommon.OrderPlaced;
using BookStoreRepository.IRepository;

namespace BookStoreRepository.Repository
{
    public class OrderSummaryRepo : IOrderSummaryRepo
    {
        private readonly IConfiguration iconfiguration;
        public OrderSummaryRepo(IConfiguration iconfiguration)
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
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId)
        {
            try
            {
                Connection();
                List<SummaryOrder> summaryOrder = new List<SummaryOrder>();
                SqlCommand com = new SqlCommand("spOrderSummary", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    summaryOrder.Add(
                        new SummaryOrder()
                        {
                            SummaryId = Convert.ToInt32(dr["SummaryId"]),
                            OrderId = Convert.ToInt32(dr["OrderId"]),
                            OrderPlaced = new PlaceOrder()
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                CustomerId = Convert.ToInt32(dr["CustomerId"]),
                                CartId = Convert.ToInt32(dr["CartId"]),
                                Cart = new Carts()
                                {
                                    //UserId = Convert.ToInt32(dr["UserId"]),
                                    Count = Convert.ToInt32(dr["Count"]),
                                    Book = new Books()
                                    {
                                        BookId = Convert.ToInt32(dr["BookId"]),
                                        BookName = Convert.ToString(dr["BookName"]),
                                        BookDescription = Convert.ToString(dr["BookDescription"]),
                                        BookAuthor = Convert.ToString(dr["BookAuthor"]),
                                        BookImage = Convert.ToString(dr["BookImage"]),
                                        BookCount = Convert.ToInt32(dr["BookCount"]),
                                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                        Rating = Convert.ToInt32(dr["Rating"])

                                    },

                                }

                            }

                        }
                        );
                }
                nlog.LogDebug("Got Order Summary");
                return summaryOrder;
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

