using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogly.Models
{
    public class Blog
    {
        //Primary Key
        public int Id { get; set; }

        //foreign key
        public string? BlogUserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Updated")]
        public DateTime? Updated { get; set; }

        [Display(Name = "Blog Image")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string? ContentType { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        //Navigation Property
        [Display(Name = "Author")]
        public virtual BlogUser? BlogUser { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
