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
    /// Interaktionslogik für GeldAbheben.xaml
    /// </summary>
    public partial class GeldAbheben : UserControl
    {
        Users user;
        MainWindow mainWindow;
        List<Konto> kontos;
        Konto k;

        public GeldAbheben(MainWindow _mainWindow, Users _user)
        {
            InitializeComponent();            
            mainWindow = _mainWindow;
            user = _user;

            kontos = getKonto();
            Kontonummer.Content = kontos.FirstOrDefault().KontoID;
            kontostand.Content = kontos.FirstOrDefault().Kontostand + "€"; 
            k = kontos.FirstOrDefault();
            Kontoliste.ItemsSource = kontos;
            Kontoliste.DisplayMemberPath = "KontoID";
            Kontoliste.SelectedValuePath = "KontoID";
            if(kontos.Count > 0)
            {
                Kontoliste.SelectedIndex = 0; 
            }
        }
        public GeldAbheben()
        {
            InitializeComponent();
            
        }
        private void Zurück(object sender, RoutedEventArgs e)
        {
            mainWindow.Hauptfenster(user);
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

        private void GeldAbhebenButton(object sender, RoutedEventArgs e)
        {
            decimal Money = 0;
            
            if (k != null)
            {
                if (decimal.TryParse(MoneyAmount.Text, out Money))
                {
                    if (Money > 0)
                    {
                        using (BankEntities1 ctx = new BankEntities1())
                        {
                            Konto kt = ctx.Konto.Where(x => x.KontoID == k.KontoID).ToList().FirstOrDefault();
                            kt.Kontostand -= Money;

                            Transaktion t = new Transaktion();
                            t.Amount = Money * -1;
                            t.Comment = "Auszahlung";
                            t.Date = DateTime.Now;
                            t.KontoID = k.KontoID;
                            ctx.Transaktion.Add(t);
                            ctx.SaveChanges();
                            k = kt;
                            MoneyAmount.Text = "";
                        }
                    }
                    else {
                        MessageBox.Show("Geben sie bitte einen Positiven Betrag an");
                    }
                    
                }
            }
            kontostand.Content = k.Kontostand + "€";
        }
        

        private void Kontoliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            k = kontos.Where(x => x.KontoID == Convert.ToInt32(Kontoliste.SelectedValue.ToString())).ToList().FirstOrDefault();
            Kontonummer.Content = k.KontoID;
            kontostand.Content = k.Kontostand + "€";
        }
    }
}
