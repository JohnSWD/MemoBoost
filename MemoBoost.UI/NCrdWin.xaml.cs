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
    /// Логика взаимодействия для NCrdWin.xaml
    /// </summary>
    public partial class NCrdWin : Window
    {
        public NCrdWin()
        {
            InitializeComponent();
            decksCBox.ItemsSource = Factory.Default.GetDecksRepository().Where(d=>d.Cards.Count>=0).OrderBy(d=>d.Name);
        }
        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (decksCBox.SelectedIndex != -1)
            {
                var d = decksCBox.SelectedItem as Deck;
                var nc = new Card { Question = qstnBox.Text, Answer = answrBox.Text, DeckID=d.ID };
                Factory.Default.GetCardsRepository().Add(nc);
                DialogResult = true;
            }
        }
    }
}
