using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class WishlistBusiness : IWishlistBusiness
    {
        public readonly IWishlistRepo wishlistRepo;
        public WishlistBusiness(IWishlistRepo wishlistRepo)
        {
            this.wishlistRepo = wishlistRepo;
        }
        public Task<int> AddWishlist(Wishlist wishlist)
        {
            var result = this.wishlistRepo.AddWishlist(wishlist);
            return result;
        }
        public bool DeleteWishlist(int UserId, int BookId)
        {
            var result = this.wishlistRepo.DeleteWishlist(UserId, BookId);
            return result;
        }
        public IEnumerable<Wishlist> GetAllWishList(int UserId)
        {
            var result = this.wishlistRepo.GetAllWishList(UserId);
            return result;
        }
    }
}
