using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MTS_CRM.MODEL
{
    public class EmailCheck : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            string pattern = @"[a-z0-9A-Z]+[-._]*[a-z0-9A-Z]+@[a-z0-9A-Z]+([-][a-z0-9A-Z]+)*\.[a-z0-9A-Z]+([-][a-z0-9A-Z]+)*";
            string email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(value.ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
