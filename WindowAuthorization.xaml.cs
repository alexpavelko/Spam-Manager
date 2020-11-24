using System.Windows;
using System.Windows.Media;

namespace Email_Spammer
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public WindowAuthorization()
        {
            InitializeComponent();
            tbEmail.Text = MainWindow.Email;
            tbName.Text = MainWindow.UserName;
            passwordBox.Password = MainWindow.Password;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!tbEmail.Text.Contains("@"))
                tbEmail.BorderBrush = Brushes.Red;
            else
            {
                MainWindow window = new MainWindow(tbEmail.Text, passwordBox.Password, tbName.Text);
                this.Close();
            }
        }

        private void TbEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (tbEmail.Text.Contains("@")) tbEmail.BorderBrush = Brushes.Black;
        }
    }
}