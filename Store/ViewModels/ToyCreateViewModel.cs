using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModels
{
    public class ToyCreateViewModel
    {
        public int ToyId { get; set; }

        [Required(ErrorMessage = "Please enter а toy name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter а description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage =
                                     "Please enter а positive price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify а category")]
        public string Category { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}
