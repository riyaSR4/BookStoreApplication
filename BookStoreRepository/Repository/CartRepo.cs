using BookStoreCommon.Book;
using BookStoreCommon.Cart;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly IConfiguration iconfiguration;
        public CartRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddCart(Carts cart, int userId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddCart", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", cart.BookId);
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@Count", cart.Count);  
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
        public bool DeleteCart(int UserId, int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteCart", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
                com.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public IEnumerable<Carts> GetAllCart(int UserId)
        {
            Connection();
            List<Carts> cart = new List<Carts>();
            SqlCommand com = new SqlCommand("spGetAllCart", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", UserId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                cart.Add(
                    new Carts()
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        Count = Convert.ToInt32(dr["Count"]),
                        //UserId = Convert.ToInt32(dr["UserId"]),
                        //CartId = Convert.ToInt32(dr["CartId"]),
                        Book = new Books()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            BookName = dr["BookName"].ToString(),
                            BookDescription = dr["BookDescription"].ToString(),
                            BookAuthor = dr["BookAuthor"].ToString(),
                            BookImage = Convert.ToString(dr["BookImage"]),
                            BookPrice = Convert.ToInt32(dr["BookPrice"]),
                            Rating = Convert.ToInt32(dr["Rating"])
                        },
                    }
                    );
            }
            return cart;
        }
        public bool UpdateCart(Carts obj, int userId)
        {
            Connection();
            SqlCommand com = new SqlCommand("spUpdateCart", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", userId);
            com.Parameters.AddWithValue("@BookId", obj.BookId);
            com.Parameters.AddWithValue("@Count", obj.Count);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
