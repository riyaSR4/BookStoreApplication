using BookStoreBusiness.IBusiness;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class OrderPlacedBusiness : IOrderPlacedBusiness
    {
        public readonly IOrderPlacedRepo orderPlacedRepo;
        public OrderPlacedBusiness(IOrderPlacedRepo orderPlacedRepo)
        {
            this.orderPlacedRepo = orderPlacedRepo;
        }
        Nlog nlog = new Nlog();
        public Task<int> PlaceOrder(int UserId, int CartId, int CustomerId)
        {
            try
            {
                var result = this.orderPlacedRepo.PlaceOrder(UserId, CartId, CustomerId);
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
