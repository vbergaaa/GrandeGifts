using Microsoft.AspNetCore.Http;
using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class HamperUpdateViewModel
    {
        [Required]
        public string HamperId { get; set; } 
        [Required]
        public string HamperName { get; set; }
        [Required]
        public string HamperDescription { get; set; }
        [Required, DataType(DataType.Currency)]
        public double HamperPrice { get; set; }
        public bool Retired { get; set; }
        [Required]
        public string CategoryId { get; set; }

        public IFormFile File { get; set; }
        public string Picture { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string ReturnUrl { get; set; }
    }
}
