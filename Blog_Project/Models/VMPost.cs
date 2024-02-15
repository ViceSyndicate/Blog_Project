using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog_Project.Models
{
    public class VMPost
    {
        [Required(ErrorMessage = "Please prodive a Title")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Title needs to be 2-32 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please prodive a Text")]
        [StringLength(450, MinimumLength = 2, ErrorMessage = "Text needs to be 2-450 characters.")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
