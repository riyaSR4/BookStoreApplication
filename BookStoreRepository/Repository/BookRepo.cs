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
using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

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
        public async Task<int> AddBook(Books obj)
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
        public IEnumerable<Books> GetAllBooks()
        {
            Connection();
            List<Books> BookList = new List<Books>();
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
                    new Books()
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
        public bool UpdateBook(Books obj)
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
                SqlCommand com = new SqlCommand("spDeleteBook", con);
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
        public bool Image(IFormFile file, int BookId)
        {
            try
            {
                if (file == null)
                {
                    return false;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account(
                     "dzpesfhyv",
                     "383472912723393",
                     "A64zmnU-bBchnSNURq3tDo7zBXE");

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                var cloudinaryfilelink = uploadResult.Uri.ToString();
                Link(cloudinaryfilelink, BookId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Link(string cloudinaryfilelink, int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUploadImage", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
                com.Parameters.AddWithValue("@fileLink", cloudinaryfilelink);
                con.Open();
                var i = com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
