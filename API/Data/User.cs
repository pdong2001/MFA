using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string UserName { get; set; } = null!;

        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [MaxLength(255)]
        public string FullName { get; set; } = null!;

        [MaxLength(6)]
        [Column(TypeName = "CHAR")]
        public string AuthenCode { get; set; } = null!;

        public DateTime CodeExpireTime { get; set; }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
