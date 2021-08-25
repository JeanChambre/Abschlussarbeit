using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Banksystem
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
        MainWindow mainWindow;
        public LoginWindow(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }

        public bool PasswortKorrect()
        {
            string salt = getSalt();
            

            if (salt != "")
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password.Password, saltBytes);
                byte[] enteredHash = rfc2898DeriveBytes.GetBytes(20);
                string str = Convert.ToBase64String(enteredHash);
                string expectedHash = getHash();

                return str.Equals(expectedHash);
            }
            return false;
        }
        private string getSalt()
        {
            using (BankEntities1 ctx = new BankEntities1())
            {
                if (ctx.Login.Where(x => x.Username == Username.Text).Count() > 0)
                {
                    return ctx.Login.Where(x => x.Username == Username.Text).FirstOrDefault().Salt;
                }

                return "";
            }
        }
        private string getHash()
        {
            using (BankEntities1 ctx = new BankEntities1())
            {
                return ctx.Login.Where(x => x.Username == Username.Text).FirstOrDefault().Password_Hash;
            }
        }


        private void LoginButton(object sender, RoutedEventArgs e)
        {
            if (Username.Text != "" && Password.Password != "" && PasswortKorrect())
            {

                using (BankEntities1 ctx = new BankEntities1())
                {
                    var l = ctx.Login.Where(x => x.Username == Username.Text).ToList().FirstOrDefault();

                    if (l.isAdmin == 0)
                    {
                        mainWindow.Hauptfenster(ctx.Users.Where(x => x.UserID == l.UserID).ToList().FirstOrDefault());
                        mainWindow.user = ctx.Users.Where(x => x.UserID == l.UserID).ToList().FirstOrDefault();
                        return;
                    }

                    else if (l.isAdmin == 1)
                    {
                        mainWindow.HauptfensterAdmin(ctx.Users.Where(x => x.UserID == l.UserID).ToList().FirstOrDefault());
                        mainWindow.user = ctx.Users.Where(x => x.UserID == l.UserID).ToList().FirstOrDefault();
                        return;
                    }
                }
            }

            else if (Username.Text == "")
            {
                MessageBox.Show("Benutzername darf nicht leer sein");
            }

            else if (Password.Password == "")
            {
                MessageBox.Show("Passwort darf nicht leer sein");
            }

            else
            {
                MessageBox.Show("Kein Account mit diesen Daten");
                Password.Password = "";
            }
        }

    }
}
