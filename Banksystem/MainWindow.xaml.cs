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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public Users user;
        public MainWindow mainWindow;
        public List<Konto> kontos;
        public Konto k;
        public List<Transaktion> transaktionsfünf;
        public List<Transaktion> transaktions;


        LoginWindow loginWindow;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            LoginAnzeigen();

            //KontoLöschen();
            //BenutzerAnlegen(user);


        }
       

        public void setUserData(Users user)
        {
            
            using (BankEntities1 ctx = new BankEntities1())
            {
                kontos = ctx.Konto.Where(x => x.UserID == user.UserID).ToList();
                transaktions = ctx.Transaktion.Where(x => x.KontoID == k.KontoID).ToList();
                transaktionsfünf = transaktions.OrderByDescending(x => x.TransaktionID).ToList();
                transaktionsfünf = transaktionsfünf.Take(5).ToList();
            }
            
        }
        public void LoginAnzeigen()
        {
            loginWindow = new LoginWindow(this);
            UserControl.Content = loginWindow;
        }
        public void KontoLöschen(Users users)
        {
            KontoLöschen kontoLöschen = new KontoLöschen(this,user);
            UserControl.Content = kontoLöschen;
        }
        public void BenutzerAnlegen(Users user)
        {
            BenutzerHinzufügen benutzerHinzufügen = new BenutzerHinzufügen(this, user);
            UserControl.Content = benutzerHinzufügen;
        }
        public void BenutzerDatenAendern(Users user)
        {
            BenutzerDatenÄndern benutzerDatenAendern = new BenutzerDatenÄndern(this, user);
            UserControl.Content = benutzerDatenAendern;
        }
        public void KontoAnlegen(Users user)
        {
            KontoHinzufügen kontoHinzufügen = new KontoHinzufügen(this, user);
            UserControl.Content = kontoHinzufügen;
        }
        public void Hauptfenster(Users user)
        {
            Hauptfenster hauptfenster = new Hauptfenster(this, user);
            UserControl.Content = hauptfenster;
        }
       
        public void HauptfensterAdmin(Users user)
        {
            HauptfensterAdmin hauptfensterAdmin = new HauptfensterAdmin(this, user);
            UserControl.Content = hauptfensterAdmin;
        }
        public void GeldEinzahlen(Users user)
        {
            GeldEinzahlen geldEinzahlen = new GeldEinzahlen(this, user);
            UserControl.Content = geldEinzahlen;
        }
        public void GeldAbheben(Users user)
        {
            GeldAbheben geldAbheben = new GeldAbheben(this, user);
            UserControl.Content = geldAbheben;
        }
        public void Überweisung(Users user)
        {
            Überweisung überweisung = new Überweisung(this, user);
            UserControl.Content = überweisung;
        }
        public void KontostandAnzeigen(Users user)
        {
            Kontostand kontostand = new Kontostand(this, user);
            UserControl.Content = kontostand;

        }
    }
}
