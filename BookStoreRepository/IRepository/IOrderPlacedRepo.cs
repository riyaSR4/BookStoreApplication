﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IOrderPlacedRepo
    {
        public Task<int> PlaceOrder(int UserId, int CartId, int CustomerId);
    }
}
