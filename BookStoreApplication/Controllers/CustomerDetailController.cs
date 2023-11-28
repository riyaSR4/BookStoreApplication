﻿using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BookStoreCommon.CustomerDetail;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailController : ControllerBase
    {
        public readonly ICustomerDetailBusiness customerDetailBusiness;
        public int userid;
        public CustomerDetailController(ICustomerDetailBusiness customerDetailBusiness)
        {
            this.customerDetailBusiness = customerDetailBusiness;
        }
        [HttpPost]
        [Route("AddAddress")]
        public ActionResult AddAddress(CustomerDetails customerDetails)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerDetailBusiness.AddAddress(customerDetails, userid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Added Successfully", Data = customerDetails });
                }
                return this.BadRequest(new { Status = false, Message = "Adding address Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteAddress")]
        public ActionResult DeleteAddress(int CustomerId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.DeleteAddress(CustomerId, this.userid);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllAddress")]
        public ActionResult GetAllAddress()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.GetAllAddress(userid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Address Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Address Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateAddress")]
        public ActionResult UpdateAddress(CustomerDetails obj)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.UpdateAddress(obj, userid);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
