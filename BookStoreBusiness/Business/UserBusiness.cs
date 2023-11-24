﻿using BookStoreBusiness.IBusiness;
using BookStoreCommon.User;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class UserBusiness : IUserBusiness
    {
        public readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public Task<int> UserRegistration(UserRegister obj)
        {
            var result = this.userRepo.UserRegistration(obj);
            return result;
            
        }
        public string UserLogin(string email, string password)
        {
            var result = this.userRepo.UserLogin(email,password);
            return result;
        }
        public string ForgetPassword(string email)
        {
            var result = this.userRepo.ForgetPassword(email);
            return result;
        }
        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword)
        {
            var result = this.userRepo.ResetPassword(email,newpassword,confirmpassword);
            return result;
        }
    }
}
