using BookStoreBusiness.IBusiness;
using BookStoreCommon.User;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class UserBusiness : IUserBusiness
    {
        public readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        Nlog nlog = new Nlog();
        public Task<int> UserRegistration(UserRegister obj)
        {
            try
            {
                var result = this.userRepo.UserRegistration(obj);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public string UserLogin(string email, string password)
        {
            try
            {
                var result = this.userRepo.UserLogin(email, password);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var result = this.userRepo.ForgetPassword(email);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword)
        {
            try
            {
                var result = this.userRepo.ResetPassword(email, newpassword, confirmpassword);
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
