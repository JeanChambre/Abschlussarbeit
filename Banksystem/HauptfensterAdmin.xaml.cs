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
    /// Interaktionslogik für HauptfensterAdmin.xaml
    /// </summary>
    public partial class HauptfensterAdmin : UserControl
    {
        Users user;
        MainWindow mainWindow;
        public HauptfensterAdmin()
        {
            InitializeComponent();
        }
        public HauptfensterAdmin(MainWindow _mainWindow,Users _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.BenutzerAnlegen(user);
        }

        private void KontoAnlegenClick(object sender, RoutedEventArgs e)
        {
            mainWindow.KontoAnlegen(user);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainWindow.Hauptfenster(user);
        }
        
        private void DatenBearbeiten(object sender, RoutedEventArgs e)
        {
            mainWindow.KontoLöschen(user);
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            mainWindow.LoginAnzeigen();
        }
    }
}
