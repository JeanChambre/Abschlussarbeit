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
    /// Interaktionslogik für Hauptfenster.xaml
    /// </summary>
    public partial class Hauptfenster : UserControl
    {
        Users user;
        MainWindow mainWindow;
        List<Konto> kontos;
        Konto k;
        List<Transaktion> transaktions = null; 
        public Hauptfenster(MainWindow _mainWindow, Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
            NameLabel.Content = user.Firstname +" "+ user.Lastname;
            kontos = getKonto();
            k = kontos.FirstOrDefault();
            kontostandAnzeigen.Content = k.Kontostand + "€";
            transaktions = LetztenTransaktionen();
            LetzteTransaktion.ItemsSource = transaktions;
            Kontoliste.ItemsSource = kontos;
            Kontoliste.DisplayMemberPath = "KontoID";
            Kontoliste.SelectedValuePath = "KontoID";
            if (kontos.Count > 0)
            {
                Kontoliste.SelectedIndex = 0;
            }
        }
        private List<Transaktion> LetztenTransaktionen()
        {
            List<Transaktion> tlist = null;
            using(BankEntities1 ctx = new BankEntities1())
            {
                tlist = ctx.Transaktion.Where(x => x.KontoID == k.KontoID).ToList();
                tlist = tlist.OrderByDescending(x => x.TransaktionID).ToList();
                tlist = tlist.Take(5).ToList();
            }
            
            return tlist;

        }
        private void EinzahlenClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GeldEinzahlen(user);  
        }
        private void DatenÄndernClick(object sender, RoutedEventArgs e)
        {
            mainWindow.BenutzerDatenAendern(user);
        }
        private void AbhebenClick(object sender, RoutedEventArgs e)
        {
            mainWindow.GeldAbheben(user);
        }
        private void ÜberweisungClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Überweisung(user);
        }
        private void KontostandClick(object sender, RoutedEventArgs e)
        {
            mainWindow.KontostandAnzeigen(user);
        }
        private List<Konto> getKonto()
        {
            List<Konto> kontos = null;
            using (BankEntities1 ctx = new BankEntities1())
            {
                kontos = ctx.Konto.Where(x => x.UserID == user.UserID).ToList();
            }
            return kontos;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            using(BankEntities1 ctx = new BankEntities1())
            {
                Login login = ctx.Login.Where(x => x.UserID == user.UserID).FirstOrDefault();
                if(login.isAdmin == 1)
                {
                    mainWindow.HauptfensterAdmin(user);
                }
                else
                {
                    mainWindow.LoginAnzeigen();
                }
            }
            
            
        }
        private void Kontoliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            k = kontos.Where(x => x.KontoID == Convert.ToInt32(Kontoliste.SelectedValue.ToString())).ToList().FirstOrDefault();
            transaktions = LetztenTransaktionen();
            LetzteTransaktion.ItemsSource = transaktions;
            kontostandAnzeigen.Content = k.Kontostand + "€";
        }
    }
}
