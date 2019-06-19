using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class AddressUpdateViewModel
    {
        [Required]
        public string Id { get; set; }
        public int? UnitNumber { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string City { get; set; }
        [Required, DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        [Required]
        public StateEnum State { get; set; }
    }
}
