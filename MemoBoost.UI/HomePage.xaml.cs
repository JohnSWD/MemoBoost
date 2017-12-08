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
    public partial class HomePage : Page //completed
    {
        
        public HomePage() //use navigation service loaded to dispose of study session!!!!!!!!!!!
        {
            InitializeComponent();
            decksListBox.ItemsSource = Factory.Default.GetDecksRepository().Where(d => d.Cards.Count >=0); //is there a better way? may be turning off lazy loading?
        }

        private void Update()
        {
            decksListBox.ItemsSource = null;
            decksListBox.ItemsSource = Factory.Default.GetDecksRepository().Where(d=>d.Cards.Count>=0);
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)//checked
        {
            var v = (Deck)decksListBox.SelectedItem;
            if (v!=null)
            {
                Factory.Default.GetCardsRepository().DeleteRange(Factory.Default.GetCardsRepository().Where(c => c.DeckID == v.ID));
                v.Cards = null;
                Factory.Default.GetDecksRepository().ChangeItem(v);
                Factory.Default.GetDecksRepository().Delete(v);
            }
        }

        private void NCButton_Click(object sender, RoutedEventArgs e)//checked
        {
            NCrdWin ncw = new NCrdWin();
            if (ncw.ShowDialog().Value)
                Update();
        }

        private void NDButton_Click(object sender, RoutedEventArgs e)//checked
        {
            NDckWin ndw = new NDckWin();
            if (ndw.ShowDialog().Value)
                Update();
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)//checked
        {
            var b = (Button)sender;
            DMngrWin dmw = new DMngrWin();
            dmw.OnIdReceived?.Invoke((int)b.Tag);
            dmw.OnActionCompleted += Update;
            dmw.Show();
        }

        private void ToStudy_Click(object sender, RoutedEventArgs e)//check for cards to learn
        {
            var hl = (Hyperlink)sender;
            var d = (Deck)hl.Tag;
            StudySession.Default.CurrentSession = d.Cards.ToList();
            StudyPage sp = new StudyPage();   
            //sp.GetDeck?.Invoke(d);
            this.NavigationService.Navigate(sp);
        }
    }
}
