using BookStoreCommon.Book;
using BookStoreCommon.Wishlist;
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
    public class WishlistRepo : IWishlistRepo
    {
        private readonly IConfiguration iconfiguration;
        public WishlistRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddWishlist(Wishlist wishlist)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddWishlist", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", wishlist.BookId);
                com.Parameters.AddWithValue("@UserId", wishlist.UserId);
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
        public bool DeleteWishlist(int UserId, int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteWishList", con);
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
        public IEnumerable<Wishlist> GetAllWishList(int UserId)
        {
            Connection();
            List<Wishlist> wishlist = new List<Wishlist>();
            SqlCommand com = new SqlCommand("spGetWishList", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", UserId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                wishlist.Add(
                    new Wishlist()
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        WishlistId = Convert.ToInt32(dr["WishlistId"]),
                        book = new Books()
                        {
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
            return wishlist;
        }
    }
}
