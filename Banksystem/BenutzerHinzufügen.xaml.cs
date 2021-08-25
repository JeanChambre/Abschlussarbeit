using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Banksystem
{
    /// <summary>
    /// Interaktionslogik für BenutzerHinzufügen.xaml
    /// </summary>
    public partial class BenutzerHinzufügen : UserControl
    {
        Users user;
        MainWindow mainWindow;
        public BenutzerHinzufügen(MainWindow _mainWindow,Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool passwordCheck = PasswortKorrect();
            bool usernameCheck = UsernameKorrect();

            if (!usernameCheck)
            {
                MessageBox.Show("Dieser Benutzername ist schon vorhanden");
            }

            else if (!passwordCheck)
            {
                MessageBox.Show("Die Passwörter stimmen nicht überein");
            }


            else
            {
                using (BankEntities1 ctx = new BankEntities1())
                {
                    Users u = new Users();
                    u.Firstname = FirstnameBox.Text;
                    u.Lastname = LastnameBox.Text;
                    ctx.Users.Add(u);
                    ctx.SaveChanges();

                    Login l = new Login();
                    l.Username = UsernameBox.Text;
                    l.isAdmin = 0;
                    l.UserID = u.UserID;
                    Rfc2898DeriveBytes rfcBytes = new Rfc2898DeriveBytes(PasswortBox.Password, 32);
                    byte[] hash = rfcBytes.GetBytes(20);
                    byte[] salt = rfcBytes.Salt;
                    l.Salt = Convert.ToBase64String(salt);
                    l.Password_Hash = Convert.ToBase64String(hash);
                    ctx.Login.Add(l);
                    ctx.SaveChanges();

                    Konto k = new Konto();
                    k.Kontostand = 0;
                    k.UserID = u.UserID;
                    ctx.Konto.Add(k);
                    ctx.SaveChanges();
                }

                MessageBox.Show("Neuer Benutzer wurde angelegt");
                
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.HauptfensterAdmin(user);
        }
        private bool PasswortKorrect()
        {
            if (PasswortBox.Password != PasswortBox2.Password)
            {
                return false;
            }

            else if (PasswortBox.Password.Length == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        private bool UsernameKorrect()
        {
            using (BankEntities1 ctx = new BankEntities1())
            {
                if (ctx.Login.Where(x => x.Username == UsernameBox.Text).Count() > 0)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }
    }
}
