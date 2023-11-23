using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IUserRepo
    {
        public  Task<int> UserRegistration(UserRegister obj);
        public string UserLogin(string email, string password);
    }
}
