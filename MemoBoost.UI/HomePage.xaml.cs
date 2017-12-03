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
using MemoBoost.Logic;
namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        
        public HomePage() //is it possible to change objects in list if i use propertuchanged event?  
        {
            InitializeComponent();
            decksListBox.ItemsSource = Factory.Default.GetDecksRepository().Items;
        }

        private void DelButton_Click(object sender, RoutedEventArgs e) //a better way to delete cards?
        {
            var v = (Deck)decksListBox.SelectedItem;
            if (v!=null)
            {
                foreach(var i in v.Cards)
                {
                    Factory.Default.GetCardsRepository().Delete(i);
                }
                Factory.Default.GetDecksRepository().Delete(v);
            }
        }

        private void NCButton_Click(object sender, RoutedEventArgs e)
        {
            NCrdWin ncw = new NCrdWin();
            ncw.Show();
        }

        private void NDButton_Click(object sender, RoutedEventArgs e)
        {
            NDckWin ndw = new NDckWin();
            ndw.Show();
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            DMngrWin dmw = new DMngrWin(); 
            dmw.theid = (int)b.Tag;//questionable
            dmw.Show();
        }
    }
}
