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
    /// Логика взаимодействия для DMngrWin.xaml
    /// </summary>
    public partial class DMngrWin : Window
    {
        public int theid; //is it ok?
        public DMngrWin()
        {
            InitializeComponent();
            cardsListBox.ItemsSource = Factory.Default.GetDecksRepository().Items.FirstOrDefault(d => d.ID == theid).Cards;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CardsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = (Card)cardsListBox.SelectedItem;
            if (c != null)
                DataContext = c;
        }
    }
}
