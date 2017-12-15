using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MemoBoost.Logic;
using System.Collections.Generic;

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page //completed
    {
        IEnumerable<Deck> _decks;
        public HomePage()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            decksListBox.ItemsSource = null;
            _decks = Factory.Default.GetDecksRepository().Where(d => d.Cards.Count >= 0).Where(d=>d.UserID==StudySession.Default.CurrentUserID);
            decksListBox.ItemsSource = _decks;
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var v = (Deck)decksListBox.SelectedItem;
            if (v!=null)
            {
                Factory.Default.GetCardsRepository().DeleteRange(Factory.Default.GetCardsRepository().Where(c => c.DeckID == v.ID));
                v.Cards = null;
                Factory.Default.GetDecksRepository().ChangeItem(v);
                Factory.Default.GetDecksRepository().Delete(v);
                Update();
            }
        }

        private void NCButton_Click(object sender, RoutedEventArgs e)
        {
            NCrdWin ncw = new NCrdWin();
            if (ncw.ShowDialog().Value)
                Update();
        }

        private void NDButton_Click(object sender, RoutedEventArgs e)
        {
            NDckWin ndw = new NDckWin();
            if (ndw.ShowDialog().Value)
                Update();
        }

        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            DMngrWin dmw = new DMngrWin();
            dmw.OnDeckReceived?.Invoke((Deck)b.Tag);
            dmw.OnActionCompleted += Update;
            dmw.Show();
        }

        private void ToStudy_Click(object sender, RoutedEventArgs e)
        {
            var hl = (Hyperlink)sender;
            var d = (Deck)hl.Tag;
            StudySession.Default.CurrentSession = d.Cards.ToList();
            StudySession.Default.CurrentDeck = d;
            StudyPage sp = new StudyPage();   
            this.NavigationService.Navigate(sp);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StudySession.Default.CurrentSession = null;
            StudySession.Default.CurrentDeck=null;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            decksListBox.ItemsSource = _decks.Where(d => d.Name.ToLower().Contains(searchBox.Text.ToLower().Trim()));
        }

        private void ResButton_Click(object sender, RoutedEventArgs e)
        {
            var d = decksListBox.SelectedItem as Deck;
            if (d != null)
            {
                foreach (var card in d.Cards)
                {
                    Factory.Default.GetScheduler().Reset(card);
                }
                Update();
            }
        }
    }
}
