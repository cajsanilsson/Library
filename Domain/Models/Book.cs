using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Book
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can be maximum 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description can be maximum 100 characters")]
        public string Description { get; set; }


        public Book() { }
        public Book(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;

        }
    }
}
