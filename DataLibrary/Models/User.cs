using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; } = null!;

    }
}
