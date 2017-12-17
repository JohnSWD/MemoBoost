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
    /// Логика взаимодействия для FinalPage.xaml
    /// </summary>
    public partial class FinalPage : Page
    {
        public FinalPage()
        {
            InitializeComponent();
            Info();
        }

        public void Info()
        {
            try
            {
                var d = StudySession.Default.CurrentDeck;
                DataContext = d;
                var n = d.Cards.Where(c => DateTime.Compare(c.Next, DateTime.Now.AddDays(1).Date) < 0).Count();
                infoBlock.Text = $"{n} card(s) will become available for study today.";
            }
            catch
            {
                MessageBox.Show("An error occured. Last action was not performed.");
            }
        }
    }
}
