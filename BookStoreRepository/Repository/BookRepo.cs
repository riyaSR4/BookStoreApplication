using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;

namespace BookStoreRepository.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly IConfiguration iconfiguration;
        public BookRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddBook(Book obj)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookName", obj.BookName);
                com.Parameters.AddWithValue("@BookDescription", obj.BookDescription);
                com.Parameters.AddWithValue("@BookAuthor", obj.BookAuthor);
                com.Parameters.AddWithValue("@BookImage", obj.BookImage);
                com.Parameters.AddWithValue("@BookCount", obj.BookCount);
                com.Parameters.AddWithValue("@BookPrice", obj.BookPrice);
                com.Parameters.AddWithValue("@Rating", obj.Rating);
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
        public IEnumerable<Book> GetAllBooks()
        {
            Connection();
            List<Book> BookList = new List<Book>();
            SqlCommand com = new SqlCommand("spGetAllBooks", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                BookList.Add(
                    new Book()
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        BookName = Convert.ToString(dr["BookName"]),
                        BookDescription = Convert.ToString(dr["BookDescription"]),
                        BookAuthor = Convert.ToString(dr["BookAuthor"]),
                        BookImage = Convert.ToString(dr["BookImage"]),
                        BookCount = Convert.ToInt32(dr["BookCount"]),
                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                        Rating = Convert.ToInt32(dr["Rating"])
                    }
                    );
            }
            foreach (var data in BookList)
            {
                Console.WriteLine(data.BookId + "" + data.BookName);
            }
            return BookList;
        }
        public bool UpdateBook(Book obj)
        {
            Connection();
            SqlCommand com = new SqlCommand("spUpdateBook", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@BookId", obj.BookId);
            com.Parameters.AddWithValue("@BookName", obj.BookName);
            com.Parameters.AddWithValue("@BookDescription", obj.BookDescription);
            com.Parameters.AddWithValue("@BookAuthor", obj.BookAuthor);
            com.Parameters.AddWithValue("@BookImage", obj.BookImage);
            com.Parameters.AddWithValue("@BookCount", obj.BookCount);
            com.Parameters.AddWithValue("@BookPrice", obj.BookPrice);
            com.Parameters.AddWithValue("@Rating", obj.Rating);
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
        public bool DeleteBook(int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
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

    }
}
