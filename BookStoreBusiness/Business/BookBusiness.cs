using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class BookBusiness : IBookBusiness
    {
        public readonly IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public Task<int> AddBook(Books obj)
        {
            var result = this.bookRepo.AddBook(obj);
            return result;
        }
        public IEnumerable<Books> GetAllBooks()
        {
            var result = this.bookRepo.GetAllBooks();
            return result;
        }
        public bool UpdateBook(Books obj)
        {
            var result = this.bookRepo.UpdateBook(obj);
            return result;
        }
        public bool DeleteBook(int BookId)
        {
            var result = this.bookRepo.DeleteBook(BookId);
            return result;
        }
        public bool Image(IFormFile file, int BookId)
        {
            var result = this.bookRepo.Image(file,BookId);
            return result;
        }
    }
}
