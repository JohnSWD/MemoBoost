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
    public delegate void GetDeckCallback(Deck deck);
    public delegate void ParallelUpdate();
    /// <summary>
    /// Логика взаимодействия для DMngrWin.xaml
    /// </summary>
    public partial class DMngrWin : Window //disable drop availability when there is no selecteditem
    {
        public GetDeckCallback OnDeckReceived;
        public ParallelUpdate OnActionCompleted;
        private Deck _thedeck;
        private Card _currentCard;
        public DMngrWin()
        {
            InitializeComponent();
            OnDeckReceived += GetDeck;
            OnActionCompleted += ToInitialState;
        }

        private void ToInitialState()
        {
            qstnBox.Clear();
            answrBox.Clear();
            cardsListBox.SelectedIndex = -1;
            DataContext = null;
            changeButton.IsEnabled = false;
            ToPartlyIntialState();
        }

        private void ToPartlyIntialState()
        {
            qstnBox.Focusable = false;
            answrBox.Focusable = false;
            //deckBox.Focusable = false;
            answrBox.AllowDrop = false;
            qstnBox.AllowDrop = false;
            raImageButton.Visibility = Visibility.Hidden;
            rqImageButton.Visibility = Visibility.Hidden;
        }

        private void GetDeck(Deck deck)
        { 
            _thedeck = deck;
            deckBox.Text = deck.Name;
            Update();
        }

        private void Update()
        {
            _thedeck.Cards=Factory.Default.GetCardsRepository().Where(c => c.DeckID == _thedeck.ID).ToList();
            cardsListBox.ItemsSource = _thedeck.Cards;
            Factory.Default.GetDecksRepository().ChangeItem(_thedeck);
            OnActionCompleted?.Invoke();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var c = cardsListBox.SelectedItem as Card;
            if(c!=null)
           {
                Factory.Default.GetCardsRepository().Delete(c);
                Update();
            }
        }

        private void CardsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToPartlyIntialState();
            _currentCard = (Card)cardsListBox.SelectedItem;
            if (_currentCard != null)
            {
                changeButton.IsEnabled = true;
                if(_currentCard.ASource!=null)
                    raImageButton.Visibility = Visibility.Visible;
                if(_currentCard.QSource!=null)
                    rqImageButton.Visibility = Visibility.Visible;
                DataContext = _currentCard;
            }
                
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            qstnBox.Focusable = true;
            answrBox.Focusable = true;
            changeButton.IsEnabled = false;
            if(_currentCard.ASource==null)
                answrBox.AllowDrop = true;
            if(_currentCard.QSource==null)
                qstnBox.AllowDrop = true;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
        }

        private void Box_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string source;
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    var file = files[0];
                    var s = (TextBox)sender;
                    Factory.Default.GetMediaManager().Copy(file, out source);
                    if ((string)s.Tag == "a")
                    {
                        _currentCard.ASource = source;
                        answrImage.Source = new BitmapImage(new Uri(source));
                    }
                    else
                    {
                        _currentCard.QSource = source;
                        qstnImage.Source = new BitmapImage(new Uri(source));
                    }
                    s.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Non-image files cannot be attached.");
            }
        }

        private void Box_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            var b = (Button)sender;
            if((string)b.Tag=="q")
            {
                path = _currentCard.QSource;
                _currentCard.PQSource = null;
                rqImageButton.Visibility = Visibility.Hidden;
                Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
                qstnImage.Source = null;
            }
            else
            {
                path = _currentCard.ASource;
                _currentCard.PASource = null;
                raImageButton.Visibility = Visibility.Hidden;
                Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
                answrImage.Source = null;
            }
            Factory.Default.GetMediaManager().ToBeDisposed(path);
        }

        private void DeckBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(deckBox.Text))
            {
                _thedeck.Name = deckBox.Text;
                Factory.Default.GetDecksRepository().ChangeItem(_thedeck);
                Update();
            }
        }
    }
}
