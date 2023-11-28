using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BookStoreCommon.Feedback;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public readonly IFeedbackBusiness feedbackBusiness;
        public int userid;
        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }
        [HttpPost]
        [Route("AddFeedback")]
        public ActionResult AddFeedback(Feedbacks feedback)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.feedbackBusiness.AddFeedback(feedback, userid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Added Successfully", Data = feedback });
                }
                return this.BadRequest(new { Status = false, Message = "Adding cart Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
    }
}
