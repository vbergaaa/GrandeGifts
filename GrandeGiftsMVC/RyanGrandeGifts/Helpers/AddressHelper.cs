using Microsoft.AspNetCore.Mvc.Rendering;
using RyanGrandeGifts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Helpers
{
    public static class AddressHelper
    {
        public static string AddressToString(Address address)
        {    
            string str = "";
            if (address.UnitNumber != null)
                str += address.UnitNumber + "/";
            
            str += address.StreetNumber + " " +
                    address.StreetName + ", " +
                    address.PostCode + " " +
                    address.Suburb + "," +
                    address.City + " " +
                    address.State;
            return str;
        }

        public static string AddressToShortString(Address address)
        {    
            string str = "";
            if (address.UnitNumber != null)
                str += address.UnitNumber + "/";

            str += address.StreetNumber + " " +
                    address.StreetName + ", " +
                    address.City + " ";
            return str;
        }
        public static SelectList AddressesToSelectList(IEnumerable<Address> addresses)
        {
            IEnumerable<SelectListItem> selectListItems = addresses.Select(a => new SelectListItem { Text = AddressToShortString(a), Value = a.AddressId });
            return new SelectList(selectListItems, "Value", "Text");
        }
    } 
}
