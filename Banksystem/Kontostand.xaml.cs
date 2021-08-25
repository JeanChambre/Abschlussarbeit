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
    /// Interaktionslogik für Kontostand.xaml
    /// </summary>
    public partial class Kontostand : UserControl
    {
        Users user;
        MainWindow mainWindow;
        List<Konto> kontos;
        Konto k;
        List<Transaktion> transaktions = null;
        public Kontostand()
        {
            InitializeComponent();
        }
        public Kontostand(MainWindow _mainWindow,Users _user)
        {
            InitializeComponent();
            user = _user;
            mainWindow = _mainWindow;
            kontos = getKonto();
            k = kontos.FirstOrDefault();
            transaktions = getTransaktionen();
            AlleTransaktion.ItemsSource = transaktions;
            aktuellesKonto.Content = k.KontoID;
            Kontoliste.ItemsSource = kontos;
            Kontoliste.DisplayMemberPath = "KontoID";
            Kontoliste.SelectedValuePath = "KontoID";
            if (kontos.Count > 0)
            {
                Kontoliste.SelectedIndex = 0;
            }
        }
        
        private List<Konto> getKonto()
        {
            List<Konto> ks = null;
            using (BankEntities1 ctx = new BankEntities1())
            {
                ks = ctx.Konto.Where(x => x.UserID == user.UserID).ToList();
            }
            
            return ks;
        }
        private List<Transaktion> getTransaktionen()
        {
            List<Transaktion> tlist = null;
            using (BankEntities1 ctx = new BankEntities1())
            {
                tlist = ctx.Transaktion.Where(x => x.KontoID == k.KontoID).ToList();
                tlist = tlist.OrderByDescending(x => x.TransaktionID).ToList();
                
            }

            return tlist;

        }
        private void Kontoliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            k = kontos.Where(x => x.KontoID == Convert.ToInt32(Kontoliste.SelectedValue.ToString())).ToList().FirstOrDefault();
            transaktions = getTransaktionen();
            AlleTransaktion.ItemsSource = transaktions;
            aktuellesKonto.Content = k.KontoID;
        }

        private void ZurueckClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Hauptfenster(user);
        }
    }
}
