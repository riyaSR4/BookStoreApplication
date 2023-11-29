using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class BookBusiness : IBookBusiness
    {
        public readonly IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        Nlog nlog = new Nlog();
        public Task<int> AddBook(Books obj)
        {
            try
            {
                var result = this.bookRepo.AddBook(obj);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Books> GetAllBooks()
        {
            try
            {
                var result = this.bookRepo.GetAllBooks();
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateBook(Books obj)
        {
            try
            {
                var result = this.bookRepo.UpdateBook(obj);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookRepo.DeleteBook(BookId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool Image(IFormFile file, int BookId)
        {
            try
            {
                var result = this.bookRepo.Image(file, BookId);
                return result;
            }
            catch(Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
