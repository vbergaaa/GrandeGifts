using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Models
{
    public class Address
    {
        public string AddressId { get; set; }
        public int? UnitNumber { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public StateEnum State { get; set; }
        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
    }

    public enum StateEnum
    {
        NSW,
        QLD,
        VIC,
        TAS,
        NT,
        SA,
        WA,
        ACT
    }
}
