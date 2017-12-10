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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для AnswerPage.xaml
    /// </summary>
    public partial class AnswerPage : Page
    {
        public delegate void OnCardReceived(Card card);
        private Card _card;
        public OnCardReceived GetCard;
        public AnswerPage()
        {
            InitializeComponent();
            GetCard += CardToModify;
        }

        private void CardToModify(Card card)
        {
            _card = card;
            if (card.State > 0)
                hardButton.Visibility = Visibility.Visible;
            DataContext = _card;
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            var b = (Button)sender;
            int q = Convert.ToInt32(b.Tag);
            ScheduleManager.CardVerdict(_card, q);
            NavigationService.Navigate(new Uri("StudyPage.xaml", UriKind.Relative));
        } 
    }
}
