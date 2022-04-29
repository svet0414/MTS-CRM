using System.Windows;

namespace MTS_CRM.DESKTOP
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static LoginService.ILoginService loginCheck = new LoginService.LoginServiceClient();
        private MainWindow mainWindow;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var logCtrl = LoginController.GetInstance();
            //servics final version
             bool isPasswordMatched = loginCheck.LoginCheck(usernameTXT.Text, passwordTXT.Password);
            //controller v1
            // bool isPasswordMatched = logCtrl.LoginCheck(usernameTXT.Text, passwordTXT.Password);

            if (isPasswordMatched) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close(); }
                     else {
                        MessageBox.Show("Wrong username or passowrd. Please try again !");
                     }

           
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (usernameTXT.Text.Equals("Username"))
                usernameTXT.Text = "";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTXT.Password.Equals("Password"))
                passwordTXT.Password = "";
        }

        private void passwordTXT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTXT.Password.Length == 0)
            {
                passwordTXT.Password = "Password";
            }

        }

        private void usernameTXT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (usernameTXT.Text.Length == 0)
            {
                usernameTXT.Text = "Username";
            }
        }
    }
}
