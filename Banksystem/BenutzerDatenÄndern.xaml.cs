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
    /// Interaktionslogik für BenutzerDatenÄndern.xaml
    /// </summary>
    public partial class BenutzerDatenÄndern : UserControl
    {
        MainWindow mainWindow;
        Users user;
        Login login;
        BankEntities1 ctx = new BankEntities1();
        public BenutzerDatenÄndern()
        {
            InitializeComponent();
        }
        public BenutzerDatenÄndern(MainWindow _mainWindow, Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
            UserGrid.DataContext = user;
            login = ctx.Login.Where(x => x.UserID == user.UserID).FirstOrDefault();
            LoginGrid.DataContext = login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ctx.Login.Where(x => x.Username == txtUsername.Text).ToList().Count > 0)
            {
                MessageBox.Show("Username bereits vergeben");

            }
            else
            {
                ctx.SaveChanges();
            }
            
        }
        private void PasswortAendern(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test");
            if (CheckPassword() && comparePassword() )
            {
                Rfc2898DeriveBytes rfcBytes = new Rfc2898DeriveBytes(txtPasswort.Password, 32);
                byte[] hash = rfcBytes.GetBytes(20);
                byte[] salt = rfcBytes.Salt;
                login.Salt = Convert.ToBase64String(salt);
                login.Password_Hash = Convert.ToBase64String(hash);
                ctx.SaveChanges();
            }
           
        }
        public bool CheckPassword()
        {
            bool hashesMatch;

            if (login.Salt != "")
            {
                byte[] saltBytes = Convert.FromBase64String(login.Salt);
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(txtPasswortalt.Password, saltBytes);
                byte[] enteredHash = rfc2898DeriveBytes.GetBytes(20);
                string str = Convert.ToBase64String(enteredHash);
                string expectedHash = login.Password_Hash;

                hashesMatch = str.Equals(expectedHash);
            }
            else
            {
                hashesMatch = false;
            }

            return hashesMatch;
        }
        private bool comparePassword()
        {
            if (txtPasswort.Password != txtPasswort2.Password)
            {
                MessageBox.Show("Die angegebenen Passwörter stimmen nicht über ein");
                return false;
            }

            else if (txtPasswort.Password.Length == 0)
            {
                MessageBox.Show("Das Passwortfeld darf nicht leer sein");
                return false;
            }

            else
            {
                return true;
            }
        }

        private void Zurück(object sender, RoutedEventArgs e)
        {
            mainWindow.Hauptfenster(user);
        }
    }
}
