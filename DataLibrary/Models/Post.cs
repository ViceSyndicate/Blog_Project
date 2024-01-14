using DataLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User User{ get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
