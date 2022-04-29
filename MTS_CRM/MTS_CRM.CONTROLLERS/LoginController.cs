using MTS_CRM.CONTROLLERS.Interfaces;
using MTS_CRM.CONTROLLERS.PasswordEncryption;
using MTS_CRM.DB;
using System.Linq;

namespace MTS_CRM.CONTROLLERS
{
    public class LoginController : ILoginController
    {
        private static LoginController loginCtrlInstance;

        private LoginController() { }

        public static LoginController GetInstance()
        {
            if (loginCtrlInstance == null)
            {
                loginCtrlInstance = new LoginController();
            }
            return loginCtrlInstance;
        }

        public bool LoginCheck(string username, string password)
        {
            using (var db = new MTSContext())
            {
                var hashedPW = SecurePassword.Hash(password);
                if (SecurePassword.Verify(password, hashedPW))
                {
                    return db.Employees.Any(x => x.Username == username || x.Password == hashedPW);

                }
                else
                {
                    return false;
                }
            }
        }
        public void GetPWFromDB(string username)
        {
            using (var db = new MTSContext())
            {
                db.Employees.Any((x => x.Username == username));

            }


        }
    }
}
