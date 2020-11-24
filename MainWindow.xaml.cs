using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Threading.Tasks;

namespace Email_Spammer
{
    public partial class MainWindow : Window
    {
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string UserName { get; set; }
        public static string AdressTo { get; set; }
        public static string Subject { get; set; }
        public static string Message { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            listBox.Items.Add("1");
            listBox.Items.Add("5");
            listBox.Items.Add("10");
            listBox.Items.Add("25");
            listBox.Items.Add("50");
            listBox.Items.Add("500");
            listBox.Items.Add("1000");
        }
        public MainWindow(string _Email, string _Password, string _UserName)
        {
            InitializeComponent();

            Email = _Email;
            Password = _Password;
            UserName = _UserName;
           
            
        }
        public MainWindow(string adressTo, string subject, string message, bool isMessage)
        {
            InitializeComponent();

            AdressTo = adressTo;
            Subject = subject;
            Message = message;
        }

        private static bool Validate()
        {
            if (Email != null && UserName != null && AdressTo != null)
                return true;
            else return false;
        }
        private static void CloseSession()
        {
            AdressTo = null;
            Subject = null;
            Message = null;
        }
        private static void CreateMessage(int numberOfMessages)
        {
            if (Validate())
            {
                MailAddress from = new MailAddress(Email, UserName);

                MailAddress to = new MailAddress(AdressTo);

                MailMessage m = new MailMessage(from, to);
                m.Subject = Subject;
                m.Body = Message;

                m.IsBodyHtml = true;
                SendEmailAsync(numberOfMessages, m).GetAwaiter();
            }
            else MessageBox.Show("(Email = null || UserName = null || AdressTo = null");
        }
        private static async Task SendEmailAsync(int numberOfMessages, MailMessage message)
        {

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(Email, Password);
            smtp.EnableSsl = true;
            for (int i = 0; i < numberOfMessages; i++)
            {
                await smtp.SendMailAsync(message);
                //MessageBox.Show($"Письмо отправлено {i}-й раз. Всего раз: {numberOfMessages}");
            }
            MessageBox.Show($"Delivered {AdressTo}!", "Message Delivered",MessageBoxButton.OK,MessageBoxImage.Information);
            CloseSession();
        }


        private void ButtonMessage_Click(object sender, RoutedEventArgs e)
        {
            var message = new WindowMessage();
            message.ShowDialog();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            var authorization = new WindowAuthorization();
            authorization.ShowDialog();
            TextBlockUserName.Visibility = Visibility.Visible;
            TextBlockUserName.Text = Email;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            int numberOfMessages;
            if (listBox.SelectedItem != null)
                numberOfMessages = Convert.ToInt32(listBox.SelectedItem);
            else numberOfMessages = 1;

            CreateMessage(numberOfMessages);
        }
    }
}