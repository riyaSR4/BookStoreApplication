using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusiness.Business
{
    public class CartBusiness
    {
        public readonly ICartRepo cartRepo;
        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }
    }
}
