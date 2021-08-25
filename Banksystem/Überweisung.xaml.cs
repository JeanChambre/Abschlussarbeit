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
    /// Interaktionslogik für Überweisung.xaml
    /// </summary>
    public partial class Überweisung : UserControl
    {
        Users user;
        MainWindow mainWindow;
        List<Konto> kontos;
        Konto k;

        public Überweisung(MainWindow _mainWindow, Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;

            kontos = getKonto();
            
            
            kontostand.Content = kontos.FirstOrDefault().Kontostand + "€"; 
            k = kontos.FirstOrDefault();
            Kontoliste.ItemsSource = kontos;
            Kontoliste.DisplayMemberPath = "KontoID";
            Kontoliste.SelectedValuePath = "KontoID";
            if (kontos.Count > 0)
            {
                Kontoliste.SelectedIndex = 0;
            }

        }

        private void KontoWechseln(object sender, RoutedEventArgs e)
        {

            k = kontos.Where(x => x.KontoID == Convert.ToInt32(Kontoliste.SelectedValue.ToString())).ToList().FirstOrDefault();
            
            kontostand.Content = k.Kontostand;
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
        private void Zurück(object sender, RoutedEventArgs e)
        {
            mainWindow.Hauptfenster(user);
        }
        private void GeldÜberweisen(object sender, RoutedEventArgs e)
        {
            decimal Money = 0;
            int kontoIDEmpfänger = -1;
            string comment = "";
            if (k != null)
            {
                if (decimal.TryParse(MoneyAmount.Text, out Money))
                {
                    using (BankEntities1 ctx = new BankEntities1())
                    {

                        if(int.TryParse(KontoIDTextbox.Text, out kontoIDEmpfänger)) 
                        {
                            if(kontoIDEmpfänger != -1)
                            {
                                Konto ks = ctx.Konto.Where(x => x.KontoID == k.KontoID).ToList().FirstOrDefault();
                                ks.Kontostand -= Money;

                                Transaktion tSender = new Transaktion();
                                tSender.Amount = Money * -1;
                                if(CommentTextbox.Text != "")
                                {
                                    comment = " \" " + CommentTextbox.Text + " \" ";
                                }
                                tSender.Comment = "Überweisung an " + getKontoInhaber(kontoIDEmpfänger) + comment;
                                tSender.Date = DateTime.Now;
                                tSender.KontoID = k.KontoID;
                                ctx.Transaktion.Add(tSender);
                                ctx.SaveChanges();
                                Konto ke = ctx.Konto.Where(x => x.KontoID == kontoIDEmpfänger).ToList().FirstOrDefault();
                                ke.Kontostand += Money;

                                Transaktion tEmpfänger = new Transaktion();
                                tEmpfänger.Amount = Money;
                                tEmpfänger.Comment = "Überweisung von " + getKontoInhaber(k.KontoID) + " \" " + CommentTextbox.Text + " \" ";
                                tEmpfänger.Date = DateTime.Now;
                                tEmpfänger.KontoID = kontoIDEmpfänger;
                                ctx.Transaktion.Add(tEmpfänger);
                                ctx.SaveChanges();
                                k = ks;
                            }
                            else
                            {
                                MessageBox.Show("Konto nicht vorhanden");
                            }
                       
                        }
                    }
                }
            }
            MoneyAmount.Text = "";
            KontoIDTextbox.Text = "";
            kontostand.Content = k.Kontostand + "€";


        }
        private string getKontoInhaber(int KontoID)
        {
            string stringname = "";
            using (BankEntities1 ctx = new BankEntities1())
            {
                Konto ks = ctx.Konto.Where(x => x.KontoID == KontoID).ToList().FirstOrDefault();
                Users u = ctx.Users.Where(x => x.UserID == ks.UserID).ToList().FirstOrDefault();
                stringname = u.Firstname + " " + u.Lastname;

            }
            return stringname;
        }

       
    }
}
