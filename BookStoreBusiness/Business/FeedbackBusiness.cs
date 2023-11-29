using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using BookStoreCommon.Feedback;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        public readonly IFeedbackRepo feedbackRepo;
        public FeedbackBusiness(IFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }
        Nlog nlog = new Nlog();
        public Task<int> AddFeedback(Feedbacks feedback, int userId)
        {
            try
            {
                var result = this.feedbackRepo.AddFeedback(feedback, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Feedbacks> GetAllFeedback(int UserId)
        {
            try
            {
                var result = this.feedbackRepo.GetAllFeedback(UserId);
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
