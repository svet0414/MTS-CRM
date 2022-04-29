using System;

namespace MTS_CRM.CONTROLLERS.ErrorCatcher
{
    //purpose of the class is to catch errors(wrong user input typically)
    //or if the user of the program leaves one of the fields blank(catch nulls)
    //of is he leaves a whitespace or emptystring
    public static class CatchError
    {
        public static void IfNameIsNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
        public static void IfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception($"The user {name} input is empty/null ");
                //follows the $ sign, then the format string literal is that token.
            }
        }
        public static void IfNullOrWhiteSpace(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("The user input for name is white space");
            }

        }
        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if ((dt.Month != System.DateTime.Now.Month) || (dt.Day < 1 && dt.Day > 31) || dt.Year != System.DateTime.Now.Year)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
