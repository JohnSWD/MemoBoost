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
    /// Логика взаимодействия для NewUserWIn.xaml
    /// </summary>
    public partial class NewUserWIn : Window
    {
        public NewUserWIn()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Factory.Default.GetUsersRepository().Items.Any(u => u.Name.ToLower() == nameBox.Text.ToLower()))
            {
                var user = new User{ Name = nameBox.Text };
                Factory.Default.GetUsersRepository().Add(user);
                DialogResult = true;
            }
            else
                MessageBox.Show("A user with this name already exists.");
        }
    }
}
