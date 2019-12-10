using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShopLibrary
{
    public class Validator
    {
        public bool checkNameAndPhoneNumber(String name, String phonenumber)
        {
            if(name == ""  || phonenumber == "")
            {
                return false;
            }
            return true;
        }
    }
}