using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    /// Interaktionslogik für KontoLöschen.xaml
    /// </summary>
    public partial class KontoLöschen : UserControl
    {
        private BankEntities1 ctx = new BankEntities1();
        private ICollectionView List;
        private Users u;
        private List<Konto> ks;
        private Konto k;
        private List<Konto> kontosaktuelle;
        Login l;
        Users user;
        MainWindow mainWindow;
        public KontoLöschen(MainWindow _mainWindow, Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
            ctx.Users.Load();
            List = CollectionViewSource.GetDefaultView(ctx.Users.Local);
            ks = ctx.Konto.ToList();
            u = (Users)List.CurrentItem;
            kontosaktuelle = ks.Where(x => x.UserID == u.UserID).ToList();
            Kontoliste.ItemsSource = kontosaktuelle;
            updateData();
            parentGrid.DataContext = List;
            if (kontosaktuelle.Count > 0)
            {
                Kontoliste.SelectedIndex = 0;
            }
        }
        private void BtForward_Click(object sender, RoutedEventArgs e)
        {
            

            //to reset selected value
            Kontoliste.SelectedIndex = -1;
            List.MoveCurrentToNext();
           
            if (List.IsCurrentAfterLast)
            {
                List.MoveCurrentToLast();
            }
            updateData();
        }
        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            

            //to reset selected value
            Kontoliste.SelectedIndex = -1;

            List.MoveCurrentToPrevious();
            
            if (List.IsCurrentBeforeFirst)
            {
                List.MoveCurrentToFirst();
            }
            updateData();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
        }
        private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = TbFilter.Text.ToLower();
            List.Filter = (x => ((Users)x).Lastname.ToLower().Contains(filter));
        }
        private void updateData()
        {
            u = (Users)List.CurrentItem;
            kontosaktuelle = ks.Where(x => x.UserID == u.UserID).ToList();
            l = ctx.Login.Where(x => x.UserID == u.UserID).FirstOrDefault();
            tbUsername.DataContext = l;
            Kontoliste.ItemsSource = kontosaktuelle;
            if (kontosaktuelle.Count > 0)
            {
                Kontoliste.SelectedIndex = 0;
            }
        }
        private void Kontoliste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kontosaktuelle.Count > 0 && Kontoliste.SelectedValue != null)
            {
                k = ks.Where(x => x.KontoID == Convert.ToInt32(Kontoliste.SelectedValue.ToString())).ToList().FirstOrDefault();
                tbKontostand.DataContext = k;
            }
        }

        private void ZurückClick(object sender, RoutedEventArgs e)
        {
            mainWindow.HauptfensterAdmin(user);
        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rfc2898DeriveBytes rfcBytes = new Rfc2898DeriveBytes("$demo123", 32);
            byte[] hash = rfcBytes.GetBytes(20);
            byte[] salt = rfcBytes.Salt;
            l.Salt = Convert.ToBase64String(salt);
            l.Password_Hash = Convert.ToBase64String(hash);
            ctx.SaveChanges();
            string str = string.Format($"Das Passwort von {u.Firstname} {u.Lastname} wurde auf das Standartpasswort zurück gesetzt");
            MessageBox.Show(str);


        }
    }
}

