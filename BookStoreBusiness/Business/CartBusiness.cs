using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class CartBusiness : ICartBusiness
    {
        public readonly ICartRepo cartRepo;
        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        public Task<int> AddCart(Carts cart, int userId)
        {
            var result = this.cartRepo.AddCart(cart, userId);
            return result;
        }
        public bool DeleteCart(int UserId, int BookId)
        {
            var result = this.cartRepo.DeleteCart(UserId, BookId);
            return result;
        }
        public IEnumerable<Carts> GetAllCart(int UserId)
        {
            var result = this.cartRepo.GetAllCart(UserId);
            return result;
        }
        public bool UpdateCart(Carts obj, int userId)
        {
            var result = this.cartRepo.UpdateCart(obj, userId);
            return result;
        }
    }
}
