using BookStoreCommon.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IFeedbackRepo
    {
        public Task<int> AddFeedback(Feedbacks feedback, int userId);
    }
}
