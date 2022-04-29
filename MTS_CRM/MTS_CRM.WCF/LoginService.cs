using MTS_CRM.CONTROLLERS;

namespace MTS_CRM.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class LoginService : ILoginService
    {
        public bool LoginCheck(string username, string password)
        {
            return LoginController.GetInstance().LoginCheck(username, password);
        }

    }
}
