using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientAPI
{
    class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return "ID: " + Id + "\n" +
            "User name: " + UserName + "\n" +
            "Email: " + Email + "\n" +
            "Password: " + Password + "\n";
        }
    }
}