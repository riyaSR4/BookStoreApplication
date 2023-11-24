using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
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
        public Task<int> AddBook(Book obj)
        {
            var result = this.bookRepo.AddBook(obj);
            return result;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            var result = this.bookRepo.GetAllBooks();
            return result;
        }
        public bool UpdateBook(Book obj)
        {
            var result = this.bookRepo.UpdateBook(obj);
            return result;
        }
        public bool DeleteBook(int BookId)
        {
            var result = this.bookRepo.DeleteBook(BookId);
            return result;
        }
    }
}
