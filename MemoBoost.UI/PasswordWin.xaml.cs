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
using System.Security.Cryptography;
using System.Windows.Shapes;

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для PasswordWin.xaml
    /// </summary>
    public partial class PasswordWin : Window
    {
        public GetUserCallback OnUserReceived;
        private User _user;
        public PasswordWin()
        {
            InitializeComponent();
            OnUserReceived += GetUser;
        }

        private void GetUser(User user)
        {
            _user = user;
            DataContext = _user;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(passwordBox.Password) && passwordBox.Password.Length<=20 && passwordBox.Password.Length>=1)
            {
                _user.Password = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(passwordBox.Password)));
                Factory.Default.GetUsersRepository().ChangeItem(_user);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Некорректная длина пароля.");
            }
        }
    }
}
