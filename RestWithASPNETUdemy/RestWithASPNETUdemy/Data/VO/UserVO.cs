using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.VO
{
    public class UserVO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiryType { get; set; }
    }
}
