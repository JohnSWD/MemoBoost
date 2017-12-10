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
    public delegate void GetIdCallback(int id);
    public delegate void ParallelUpdate();
    /// <summary>
    /// Логика взаимодействия для DMngrWin.xaml
    /// </summary>
    public partial class DMngrWin : Window //disable drop availability when there is no selecteditem
    {
        public GetIdCallback OnIdReceived;
        public ParallelUpdate OnActionCompleted;
        private int _theid;
        private Card _currentCard;
        public DMngrWin()
        {
            InitializeComponent();
            OnIdReceived += GetId;
            OnActionCompleted += ToInitialState;
        }

        private void ToInitialState()
        {
            qstnBox.Clear();
            answrBox.Clear();
            cardsListBox.SelectedIndex = -1;
            DataContext = null;
            ToPartlyIntialState();
        }

        private void ToPartlyIntialState()
        {
            qstnBox.Focusable = false;
            answrBox.Focusable = false;
            //deckBox.Focusable = false;
            changeButton.IsEnabled = true;
        }

        private void GetId(int id)
        {
            _theid = id;
            Update();
        }

        private void Update()
        {
            cardsListBox.ItemsSource = Factory.Default.GetDecksRepository().Where(d => d.Cards.Count >= 0).Where(d=>d.ID==_theid).ToList()[0].Cards;
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
            var c = (Card)cardsListBox.SelectedItem;
            if (c != null)
            {
                DataContext = c;
                _currentCard = c;
            }
                
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            qstnBox.Focusable = true;
            answrBox.Focusable = true;
            //deckBox.Focusable = true;
            changeButton.IsEnabled = false;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)//is it ok
        {
            _currentCard.Question = qstnBox.Text;
            _currentCard.Answer = answrBox.Text;
            Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
        }

        private void AnswrBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var file = files[0];
                var s = (TextBox)sender;
                MediaManager.Copy(file);
                s.Focus();
                //consturct string with question+image name
                
                //var df = DataFormats.Bitmap;

                
            }
        }

        private void AnswrBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
