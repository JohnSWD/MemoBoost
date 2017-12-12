﻿using MemoBoost.Logic;
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
                changeButton.IsEnabled = true;
                if(c.ASource!=null)
                    raImageButton.Visibility = Visibility.Visible;
                if(c.QSource!=null)//shows button when source is null!!!!!
                    rqImageButton.Visibility = Visibility.Visible;
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
            if(_currentCard.ASource==null)
                answrBox.AllowDrop = true;
            if(_currentCard.QSource==null)
                qstnBox.AllowDrop = true;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            _currentCard.Question = qstnBox.Text;//possibly can be simplified if _currentCard was datacontext
            _currentCard.Answer = answrBox.Text;
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
                    MediaManager.Copy(file, out source);
                    if ((string)s.Tag == "a")
                    {
                        _currentCard.ASource = source;
                        var bi = new BitmapImage(new Uri(source));
                        bi.CacheOption = BitmapCacheOption.OnLoad;//doesnt affect deletion problem
                        answrImage.Source = bi;
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
                qstnImage.Source = null;
                _currentCard.QSource = null;
                rqImageButton.Visibility = Visibility.Hidden;
                Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
            }
            else
            {
                path = _currentCard.ASource;
                answrImage.Source = null;
                _currentCard.ASource = null;
                raImageButton.Visibility = Visibility.Hidden;
                Factory.Default.GetCardsRepository().ChangeItem(_currentCard);
            }
            MediaManager.Remove(path);//doesnt work
        }
    }
}
