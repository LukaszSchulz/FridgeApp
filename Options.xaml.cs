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
using System.Windows.Shapes;

namespace FridgeWPF
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public MainWindow Window { get; }

        public Options(MainWindow window)
        {
            InitializeComponent();
            Window = window;
        }

        private void SetConnectionString(OnlineDataBase dataBase)
        {
            dataBase.SetDatabase
                (
                txtServerName.Text,
                txtDataBaseName.Text,
                txtUsername.Text,
                txtPassword.Password.ToString()
                );
        }

        private void btnSetNewServer_Click(object sender, RoutedEventArgs e)
        {
            SetConnectionString(Window.fridge.Filler.dataBasePull.DataBase);
            MessageBox.Show("You have changed your server temporarily.");
            Close();
        }
    }
}
