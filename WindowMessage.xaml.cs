using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Email_Spammer
{
    /// <summary>
    /// Логика взаимодействия для WindowMessage.xaml
    /// </summary>
    public partial class WindowMessage : Window
    {
        public WindowMessage()
        {
            InitializeComponent();
            textBoxAdress.Text = MainWindow.AdressTo;
            textBoxSubject.Text = MainWindow.Subject;
        }

        private void TextBoxAdress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxAdress.Text.Contains("@")) textBoxAdress.BorderBrush = new SolidColorBrush(Color.FromRgb(173, 171, 179));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxAdress.Text.Contains("@"))
                textBoxAdress.BorderBrush = Brushes.Red;
            else
            {
                MainWindow mainWindow = new MainWindow(textBoxAdress.Text,textBoxSubject.Text, GetString(richTextBox),true);
                this.Close();
            }
           
        }
       
        private string GetString(RichTextBox rtb)
        {
            return new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
        }
        private void ButtonAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try
            {
                openFileDialog.Filter = "TXT, HTML, DOC|*.txt; *.html; *.doc; | TXT| *.txt| HTML| *.html| DOC| *.doc";

                if (openFileDialog.ShowDialog() == true)
                {
                    string fileName = openFileDialog.FileName;
                    string fullPath = Directory.GetParent(fileName).ToString();
                    richTextBox.Document.Blocks.Clear();
                    StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(("windows-1251")));
                    string line = null;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        richTextBox.AppendText(line);
                        richTextBox.AppendText("\r\n");
                        line = sr.ReadLine();
                    }
                    sr.Close();
                    sr.Dispose();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRecentContacts_Click(object sender, RoutedEventArgs e)
        {
            var recentContacts = new WindowRecentContacts();
            if (recentContacts.ShowDialog() == true)
            {
               //... will be code
            }
        }
    }
}
