using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ViewModels
{
    public class AccountProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
