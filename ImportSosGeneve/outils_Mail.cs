using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ImportSosGeneve
{
    public class Outils_Mail
    {
        //On regarde si la chaine est une adresse Email
        public static bool isEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }

            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            else
            {
                return false; ;
            }
        }

    }
}
