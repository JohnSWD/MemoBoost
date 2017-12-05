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
    public partial class DMngrWin : Window
    {
        public GetIdCallback OnIdReceived;
        public ParallelUpdate OnActionCompleted;
        private int _theid;
        public DMngrWin()
        {
            InitializeComponent();
            OnIdReceived += GetId;
            OnActionCompleted += ToInitialState;
        }

        private void ToInitialState()
        {
            qstnBox.Clear();
            qstnBox.Focusable = false;
            answrBox.Clear();
            answrBox.Focusable = false;
            deckBox.Focusable = false;
            cardsListBox.SelectedIndex = -1;
            DataContext = null;
            saveButton.IsEnabled = false;
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



        private void SaveButton_Click(object sender, RoutedEventArgs e) //more convenient way to change? (use events to track changes?)
        {
            //if (deckBox.Text != )
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
            var c = (Card)cardsListBox.SelectedItem;
            if (c != null)
                DataContext = c;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            qstnBox.Focusable = true;
            answrBox.Focusable = true;
            deckBox.Focusable = true;
            changeButton.IsEnabled = false;
            saveButton.IsEnabled = true;
        }
    }
}
