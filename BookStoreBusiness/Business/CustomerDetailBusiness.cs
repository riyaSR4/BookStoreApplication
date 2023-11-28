using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using BookStoreCommon.CustomerDetail;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class CustomerDetailBusiness : ICustomerDetailBusiness
    {
        public readonly ICustomerDetailRepo customerDetailRepo;
        public CustomerDetailBusiness(ICustomerDetailRepo customerDetailRepo)
        {
            this.customerDetailRepo = customerDetailRepo;
        }
        public CustomerDetails AddAddress(CustomerDetails customerDetails, int userId)
        {
            var result = this.customerDetailRepo.AddAddress(customerDetails, userId);
            return result;
        }
        public bool DeleteAddress(int CustomerId, int UserId)
        {
            var result = this.customerDetailRepo.DeleteAddress(CustomerId, UserId);
            return result;
        }
        public IEnumerable<CustomerDetails> GetAllAddress(int UserId)
        {
            var result = this.customerDetailRepo.GetAllAddress(UserId);
            return result;
        }
        public bool UpdateAddress(CustomerDetails obj, int userId)
        {
            var result = this.customerDetailRepo.UpdateAddress(obj, userId);
            return result;
        }
    }
}
