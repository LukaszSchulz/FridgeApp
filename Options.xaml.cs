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
    public partial class Options : Window//okno umożliwiające zdalny wybór serwera
    {
        public MainWindow Window { get; } //przez okno główne Options ma dostęp do lodówki i narzędzi obsługujących bazę danych

        public Options(MainWindow window)
        {
            InitializeComponent();
            Window = window;
        }

        private void SetConnectionString()
        {
            if(txtServerName.Text.Length>0 &&
                txtDataBaseName.Text.Length > 0 &&
                txtUsername.Text.Length > 0 &&
                txtPassword.Password.Length > 0)
                {
                    Window.DataBase.SetDatabase //ustawia nowe parametry do łączenia zserwerem 
                    (txtServerName.Text,
                    txtDataBaseName.Text,
                    txtUsername.Text,
                    txtPassword.Password.ToString()
                    );
                MessageBox.Show("You have changed your server temporarily.");
            }
            else
            {
                MessageBox.Show("Please fill the form first.");
            }
        }

        private void btnSetNewServer_Click(object sender, RoutedEventArgs e)//obsługuje przycisk zmieniający tymczasowo ustawienia 
        {                                                                   //bazy danych
            SetConnectionString();//wykorzystuje lokalną metodę do zmiany parametrów
                                                                            //connection stringa
            Close();    //zamyka okno po wykonaniu zadania
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)//ustawia domyślną bazę danych dla aplikacji
        {
            Window.DataBase = new MySqlDataBase();//tworzy nową instancję bazy danych
            Window.DataBase.SetDatabase("sql11.freesqldatabase.com", "sql11227333", "sql11227333", "F48xDrZZcw");
                                                //podaje parametry do połączenia
            Window.IsEnabled = true;//odblokowuje ekran
            Close();    //zamyka okno
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//Przycisk obsługujący zamknięcie okna
        {
            MessageBoxResult result;
            result=MessageBox.Show("Are you sure?\nThis will cause whole application to close", //przy próbie zamknięcia pojawia
                "Closing app",                                                                    //się ostrzeżenie
                MessageBoxButton.OKCancel, 
                MessageBoxImage.Asterisk);

            if (result == MessageBoxResult.OK) //jeśli klient naciśnie ok - zamknie się cała aplikacja 
            {
                Window.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
