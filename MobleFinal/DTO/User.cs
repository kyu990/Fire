using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobleFinal.DTO
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Authority { get; set; }
        public string Tel { get; set; }
        public string ClientSerial { get; set; }
        public string ProfilePicturePath { get; set; }

        public User(string email, string name, string address, int authority, string tel, string clientSerial, string profilePicturePath)
        {
            Email = email;
            Name = name;
            Address = address;
            Authority = authority;
            Tel = tel;
            ClientSerial = clientSerial;
            ProfilePicturePath = profilePicturePath;
        }

        public User(string email, string password, string name, string address, int authority, string tel, string clientSerial, string profilePicturePath)
        {
            Email = email;
            Password = password;
            Name = name;
            Address = address;
            Authority = authority;
            Tel = tel;
            ClientSerial = clientSerial;
            ProfilePicturePath = profilePicturePath;
        }

        public User() { }
    }

    public class Authority
    {
        private int Id { get; set; }
        private string Name { get; set; }
    }
}

