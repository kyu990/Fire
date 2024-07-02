using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobleFinal.DTO
{
    internal static class Login
    {
        public static LoginUser CurrentUser { get; set; }
    }

    internal class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Authority { get; set; }
        public string Tel { get; set; }
        public string ClientSerial { get; set; }

        public LoginUser(string email, string name, string address, int authority, string tel, string clientSerial)
        {
            Email = email;
            Name = name;
            Address = address;
            Authority = authority;
            Tel = tel;
            ClientSerial = clientSerial;
        }
    }
}
