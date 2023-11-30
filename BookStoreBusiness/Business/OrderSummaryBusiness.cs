using BookStoreBusiness.IBusiness;
using BookStoreCommon.OrderSummary;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace BookStoreBusiness.Business
{
    public class OrderSummaryBusiness : IOrderSummaryBusiness
    {
        public readonly IOrderSummaryRepo orderSummaryRepo;
        public OrderSummaryBusiness(IOrderSummaryRepo orderSummaryRepo)
        {
            this.orderSummaryRepo = orderSummaryRepo;
        }
        Nlog nlog = new Nlog();
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId)
        {
            try
            {
                var result = this.orderSummaryRepo.GetOrderSummary(UserId);
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
