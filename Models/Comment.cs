using Blogly.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blogly.Models
{
    public class Comment
    {
        //Primary Key
        public int Id { get; set; }

        //foreign Keys
        public int PostId { get; set; }
        public string? BlogUserId { get; set; }
        public string? ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string? Body { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Moderated { get; set; }
        public DateTime? Deleted { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string? ModeratedBody { get; set; }
        
        public ModerationType ModerationType { get; set; }

        //Navigation Properties
        public virtual Post? Post { get; set; }
        public virtual BlogUser? BlogUser { get; set; }
        public virtual BlogUser? Moderator { get; set; }
    }
}
