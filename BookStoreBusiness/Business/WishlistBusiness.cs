using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class WishlistBusiness : IWishlistBusiness
    {
        public readonly IWishlistRepo wishlistRepo;
        public WishlistBusiness(IWishlistRepo wishlistRepo)
        {
            this.wishlistRepo = wishlistRepo;
        }
        Nlog nlog = new Nlog();
        public Task<int> AddWishlist(Wishlist wishlist, int userId)
        {
            try
            {
                var result = this.wishlistRepo.AddWishlist(wishlist, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWishlist(int UserId, int BookId)
        {
            try
            {
                var result = this.wishlistRepo.DeleteWishlist(UserId, BookId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Wishlist> GetAllWishList(int UserId)
        {
            try
            {
                var result = this.wishlistRepo.GetAllWishList(UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
