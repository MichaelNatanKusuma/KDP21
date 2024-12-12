using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinqToSQLMvcApplication.Models
{
    public class BookModel
    {
        public BookModel()
        {
            Publishers = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Author is required")]
        [DisplayName("Author")]
        public string Auther { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 9999, ErrorMessage = "Price must be between 1 and 9999")]
        public decimal Price { get; set; }
        [DisplayName("Publisher Id")]
        [HiddenInput]
        [Required(ErrorMessage = "PublisherId is required")]
        public int PublisherId { get; set; }
        [DisplayName("Publisher Name")]
        [Required(ErrorMessage = "Publisher Name is required")]
        public string PublisherName { get; set; }
        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}