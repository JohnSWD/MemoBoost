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
    /// Логика взаимодействия для StudyPage.xaml
    /// </summary>
    public partial class StudyPage : Page
    {
        private List<Card> _cards;  

        public StudyPage()
        {
            InitializeComponent();
        }

        private void CardsToStudy()
        {
            _cards = StudySession.Default.CurrentSession;
            if(_cards.Count>0)
                DataContext = _cards[0];
            else
            {
                NavigationService.Navigate(new Uri("FinalPage.xaml", UriKind.Relative));
            }
        }

        private void AnswrButton_Click(object sender, RoutedEventArgs e)
        {
            AnswerPage ap = new AnswerPage();
            ap.GetCard?.Invoke(_cards[0]);
            NavigationService.Navigate(ap);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CardsToStudy();
        }
    }
}
