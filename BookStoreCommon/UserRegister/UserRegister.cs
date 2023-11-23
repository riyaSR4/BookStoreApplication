using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommon.User
{
    public class UserRegister
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int MobileNumber { get; set; }
    }
}
