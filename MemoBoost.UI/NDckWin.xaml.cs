using MemoBoost.Logic;
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

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для NDckWin.xaml
    /// </summary>
    public partial class NDckWin : Window //deck will be connected to a certain user
    {
        public NDckWin()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Factory.Default.GetDecksRepository().Items.Any(d => d.Name.ToLower() == nameBox.Text.ToLower()))
                {
                    var deck = new Deck { Name = nameBox.Text, UserID = StudySession.Default.CurrentUserID };
                    Factory.Default.GetDecksRepository().Add(deck);
                    DialogResult = true;
                }
                else
                    MessageBox.Show("A deck with this name already exists.");
            }
            catch
            {
                MessageBox.Show("An error occured. Last action was not performed.");
            }
        }
    }
}
