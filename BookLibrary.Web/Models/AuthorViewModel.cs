using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Web.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name required")]
        [StringLength(250, ErrorMessage = "Maximum length is 250")]
        public string Name { get; set; }

        [Display(Name = "Nationality")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nationality required")]
        [StringLength(250, ErrorMessage = "Maximum length is 150")]
        public string Nacionality { get; set; }
    }
}