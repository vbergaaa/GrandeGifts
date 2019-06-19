using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.ApiModels
{
    public class CategoryApiModel
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int HamperCount { get; set; }
    }
}
