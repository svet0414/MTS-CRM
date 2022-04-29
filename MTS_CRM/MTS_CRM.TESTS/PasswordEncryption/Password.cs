using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MTS_CRM.CONTROLLERS.PasswordEncryption.Tests

{
    [TestClass()]
    public class Password
    {
        [TestMethod()]
        public void HashTest()
        {
            var password = "qwerty1234";
            string hashed_password = SecurePassword.Hash(password);
            Assert.IsTrue(SecurePassword.Verify(password, hashed_password));
            //  Assert.IsTrue(password = hashed_password);
            //  return "Password is Valid";


        }

        [TestMethod()]
        public void CheckPasswordTest()
        {
            string password = "Password1";
            // SecurePassword.CheckPassword(password);
            Assert.IsTrue(SecurePassword.CheckPassword(password));

        }

        [TestMethod()]
        public void MatchPassword()
        {
            string password = "Password1";

            // SecurePassword.CheckPassword(password);
            if (SecurePassword.CheckPassword(password))
            {

                var hashedPw = SecurePassword.Hash(password);
                if (SecurePassword.Verify(password, hashedPw))
                {
                    Console.WriteLine("successsssss");
                }
            }


        }

    }


}