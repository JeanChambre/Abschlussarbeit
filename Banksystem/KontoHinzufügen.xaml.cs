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

namespace Banksystem
{
    /// <summary>
    /// Interaktionslogik für KontoHinzufügen.xaml
    /// </summary>
    public partial class KontoHinzufügen : UserControl
    {
        Users user;
        MainWindow mainWindow;
        public KontoHinzufügen(MainWindow _mainWindow,Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int userid = -1;
            decimal kontostand = 0;
            if(int.TryParse(UserIDText.Text,out userid))
            {
                using (BankEntities1 ctx = new BankEntities1())
                {
                    Users u = ctx.Users.Where(x => x.UserID == userid).FirstOrDefault();
                    if(u != null)
                    {
                        
                        if(decimal.TryParse(KontostandText.Text,out kontostand))
                        {
                            Konto k = new Konto();
                            k.UserID = userid;
                            k.Kontostand = kontostand;
                            ctx.Konto.Add(k);
                            ctx.SaveChanges();
                            MessageBox.Show("Neues Konto hinzugefügt");
                            KontostandText.Text = "";
                            UserIDText.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Ungültiger Kontostand");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kein User mit dieser UserID gefunden");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ungültige UserID");
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.HauptfensterAdmin(user);
        }
    }
}
