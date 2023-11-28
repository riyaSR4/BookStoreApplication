using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using BookStoreCommon.Feedback;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        public readonly IFeedbackRepo feedbackRepo;
        public FeedbackBusiness(IFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }
        public Task<int> AddFeedback(Feedbacks feedback, int userId)
        {
            var result = this.feedbackRepo.AddFeedback(feedback, userId);
            return result;
        }
    }
}
