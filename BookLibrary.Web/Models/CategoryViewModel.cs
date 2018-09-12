using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Web.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Books = new List<BookViewModel>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Name required")]
        [StringLength(250, ErrorMessage ="Maximum length is 250")]
        public string Name { get; set; }

        public List<BookViewModel> Books { get; set; }
    }
}