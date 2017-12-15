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
            decksCBox.ItemsSource = Factory.Default.GetDecksRepository().Where(d=>d.Cards.Count>=0).Where(d=>d.UserID==StudySession.Default.CurrentUserID).OrderBy(d=>d.Name);
        }
        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (decksCBox.SelectedIndex != -1)
            {
                string apath=null;
                string qpath=null;
                if (answrImage.Source != null)
                    Factory.Default.GetMediaManager().Copy(answrImage.Source.ToString().Replace("file:///", ""), out apath);
                if (qstnImage.Source != null)
                    Factory.Default.GetMediaManager().Copy(qstnImage.Source.ToString().Replace("file:///", ""), out qpath);
                var d = decksCBox.SelectedItem as Deck;
                var nc = new Card { Question = qstnBox.Text, Answer = answrBox.Text, DeckID=d.ID, ASource=apath, QSource=qpath};
                Factory.Default.GetCardsRepository().Add(nc);
                DialogResult = true;
            }
        }

        private void Box_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    var file = files[0];
                    var s = (TextBox)sender;
                    if ((string)s.Tag == "q")
                        qstnImage.Source = new BitmapImage(new Uri(file));
                    else
                        answrImage.Source = new BitmapImage(new Uri(file));
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

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var path = qstnImage.Source;
            var b = (Button)sender;
            if ((string)b.Tag == "q")
                qstnImage.Source = null;
            else
                answrImage.Source = null;
        }
    }
}
