using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication13.Models
{
    public class DirectorValidation : ValidationAttribute
    {
        private Regex wrongCharacters = new Regex("^.*[0-9!@#$%^&*()+={}:;,].*");
        private Regex validationRegex = new Regex("^[A-Z][A-Za-z\\s]*$");
        public override bool IsValid(object value)
        {
            string director = (string)value;
            
            if(director == "")
            {
                return false;
            }

            if (director.Length <=3)
            {
                ErrorMessage = "Name must be longer than 3 characters";
                return false;
            }

            if (director.Length >= 80)
            {
                ErrorMessage = "Name can't be longer than 80 characters";
                return false;
            }

            if (wrongCharacters.IsMatch(director))
            {
                ErrorMessage = "Wrong data format";
                return false;
            }

            if (!validationRegex.IsMatch(director))
            {
                ErrorMessage = "Wrong data format";
                return false;
            }
            return true;
   
        }
    }
}
