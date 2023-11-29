﻿using BookStoreBusiness.IBusiness;
using BookStoreCommon.Wishlist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Utility;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPlacedController : ControllerBase
    {
        public readonly IOrderPlacedBusiness orderPlacedBusiness;
       
        public OrderPlacedController(IOrderPlacedBusiness orderPlacedBusiness)
        {
            this.orderPlacedBusiness = orderPlacedBusiness;
        }
        Nlog nlog = new Nlog();

        [HttpPost]
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(int CartId, int CustomerId)
        {
            try
            {
                var result = this.orderPlacedBusiness.PlaceOrder(CartId, CustomerId);
                if (result != null)
                {
                    nlog.LogInfo("Order Placed Successfully");
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Placing Order Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
    }
}