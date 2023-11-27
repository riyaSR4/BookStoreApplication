using BookStoreCommon.Wishlist;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IWishlistRepo
    {
        public Task<int> AddWishlist(Wishlist wishlist);
        public bool DeleteWishlist(int UserId, int BookId);
        public IEnumerable<Wishlist> GetAllWishList(int UserId);
    }
}
