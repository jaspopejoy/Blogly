using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blogly.Models
{
    public class Tag
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int PostId { get; set; }
        public string? BlogUserId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Text { get; set; }

        //Navigation Property
        public virtual Post? Post { get; set; }
        public virtual BlogUser? BlogUser { get; set; }
    }
}