using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Web.Models
{
    public class BookViewModel
    {
        public BookViewModel()
        {

        }
        public int Id { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "Select an author")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Select an author")]
        public int AuthorId { get; set; }

        [Display(Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title required")]
        [StringLength(250, ErrorMessage = "Maximum length is 250")]
        public string Title { get; set; }

        [Display(Name = "Author")]
        public string AuthorName { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price required")]        
        public decimal Price { get; set; }

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "Editorial required")]
        public string Editorial { get; set; }
    }
}