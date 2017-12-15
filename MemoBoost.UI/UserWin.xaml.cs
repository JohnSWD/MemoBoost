using MemoBoost.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для UserWin.xaml
    /// </summary>
    public partial class UserWin : Window
    {
        public UserWin()
        {
            InitializeComponent();
            DefaultUser();
            Update();
        }

        public void DefaultUser()
        {
            if ((Factory.Default.GetUsersRepository().Items.Count() == 1) && (StudySession.Default.CurrentUserID==0))
            {
                StudySession.Default.CurrentUserID = Factory.Default.GetUsersRepository().Items.ToList()[0].ID;
                StartWin sw = new StartWin();
                sw.Show();
                this.Close();
            }
        }

        private void Update()
        {
            userListBox.ItemsSource = Factory.Default.GetUsersRepository().Where(u=>u.Decks.Count>=0);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewUserWIn nuw = new NewUserWIn();
            if (nuw.ShowDialog().Value)
                Update();
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            var u = userListBox.SelectedItem as User;
            if (u != null)
            {
                RenameUserWin ruw = new RenameUserWin();
                ruw.OnUserReceived?.Invoke(u);
                if (ruw.ShowDialog().Value)
                    Update();
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var u = (User)userListBox.SelectedItem;
            if(u!=null)
            {
                var decks = Factory.Default.GetDecksRepository().Where(d=>d.Cards.Count>=0).Where(d => d.UserID == u.ID);
                DelRelatedDecks(decks, u);
                u.Decks = null;
                Factory.Default.GetUsersRepository().ChangeItem(u); 
                Factory.Default.GetUsersRepository().Delete(u);
                Update();
            }
        }

        private void DelRelatedDecks(IEnumerable<Deck> decks, User u)
        {
            foreach (var deck in decks)
            {
                Factory.Default.GetCardsRepository().DeleteRange(Factory.Default.GetCardsRepository().Where(c => c.DeckID == deck.ID));
                deck.Cards = null;
                Factory.Default.GetDecksRepository().ChangeItem(deck);
                Factory.Default.GetDecksRepository().Delete(deck);
            }

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var u = (User)userListBox.SelectedItem;
            if ((u != null && u.Password==null) || (Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(passwordBox.Password))) == u.Password))
            {
                StudySession.Default.CurrentUserID = u.ID;
                StartWin sw = new StartWin();
                sw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректные пароль.");
            }
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var u = userListBox.SelectedItem as User;
            if (u != null)
            {
                PasswordWin pw = new PasswordWin();
                pw.OnUserReceived?.Invoke(u);
                if (pw.ShowDialog().Value)
                    Update();
            }
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var u = userListBox.SelectedItem as User;
            if (u!=null)
            {
                if (u.Password!=null)
                {
                    passwordPanel.Visibility = Visibility.Visible;
                    passwordButton.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
