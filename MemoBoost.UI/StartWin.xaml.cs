using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для StartWin.xaml
    /// </summary>
    public partial class StartWin : Window
    {
        public StartWin()
        {
            InitializeComponent();
            Main.Content = new HomePage();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = (Hyperlink)sender;
                if ((string)x.Tag == "Home")
                    Main.Content = new HomePage();
            }
            catch
            {
                MessageBox.Show("An error occured. Last action was not performed.");
            }

        }

        private void Cprofile_Click(object sender, RoutedEventArgs e)
        {
            UserWin uw = new UserWin();
            uw.Show();
            this.Close();
        }

        private void Opstat_Click(object sender, RoutedEventArgs e)
        {
            StatisticksWin sw = new StatisticksWin();
            sw.Show();
        }
    }
}
