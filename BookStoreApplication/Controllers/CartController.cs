using BookStoreBusiness.IBusiness;
using BookStoreCommon.Wishlist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BookStoreCommon.Cart;
using BookStoreCommon.Book;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly ICartBusiness cartBusiness;
        public int userid;
        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }
        [HttpPost]
        [Route("AddCart")]
        public ActionResult AddCart(Carts cart)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                
                var result = this.cartBusiness.AddCart(cart,userid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Added Successfully", Data = cart });
                }
                return this.BadRequest(new { Status = false, Message = "Adding cart Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteCart")]
        public ActionResult DeleteCart(int BookId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.DeleteCart(BookId, this.userid);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Cart Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Cart Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllCart")]
        public ActionResult GetAllCart()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.GetAllCart(userid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Carts Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Carts Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateCart")]
        public ActionResult UpdateCart(Carts obj)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.UpdateCart(obj, userid);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Cart Updated Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Cart Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
