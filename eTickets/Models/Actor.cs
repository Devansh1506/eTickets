using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Profile Picture is reqired")]
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

        [Required(ErrorMessage = "Full Name is reqired")]
        [StringLength(30, MinimumLength =3, ErrorMessage ="Full Name must be between 3 and 30 chars")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is reqired")]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "Bio must be between 0 and 150 chars")]

        [Display(Name = "Biography")]
        public string Bio { get; set; }


        // Relationships
        public List<Actor_Movie>? Actor_Movies { get; set; }

    }
}
