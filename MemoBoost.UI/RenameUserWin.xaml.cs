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
    public delegate void GetUserCallback(User user);
    /// <summary>
    /// Логика взаимодействия для RenameUserWin.xaml
    /// </summary>
    public partial class RenameUserWin : Window
    {
        public GetUserCallback OnUserReceived;
        private User _user;
        public RenameUserWin()
        {
            InitializeComponent();
            OnUserReceived += GetUser;
        }

        private void GetUser(User user)
        {
            _user = user;
            DataContext= _user;//if it is two way it is supposed to change the instance as well
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Factory.Default.GetUsersRepository().Items.Any(u => u.Name.ToLower() == nameBox.Text.ToLower()))
            {
                Factory.Default.GetUsersRepository().ChangeItem(_user);
                DialogResult = true;
            }
            else
                MessageBox.Show("A user with this name already exists.");
        }
    }
}
