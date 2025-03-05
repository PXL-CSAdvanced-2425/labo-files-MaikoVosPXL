using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfAppLaboFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _fileName = "personen.csv";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(firstNameTextBox.Text) || String.IsNullOrEmpty(lastNameTextBox.Text))
            {
                MessageBox.Show("Geef een Voornaam en Achternaam in!");
            }
            else
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = firstNameTextBox.Text;
                firstNameListBox.Items.Add(listBoxItem);

                ListBoxItem listBoxItem2 = new ListBoxItem();
                listBoxItem2.Content = lastNameTextBox.Text;
                lastNameListBox.Items.Add(listBoxItem2);
            }

            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            firstNameTextBox.Focus();
            
        }

        private void readFileButton_Click(object sender, RoutedEventArgs e)
        {
            // using Microsoft.Win32;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = _fileName;
            ofd.InitialDirectory = Environment.CurrentDirectory;

            if(ofd.ShowDialog() == true) // Als true, dan geldig bestand
            {
                string fileNameFromOfd = ofd.FileName;
                using (StreamReader sr = new StreamReader(fileNameFromOfd))
                {
                    while(!sr.EndOfStream)
                    {
                        string lineOfText = sr.ReadLine();
                        string[] names = lineOfText.Split(';');
                        string firstName = names[0];
                        string lastName = names[1];
                        firstNameListBox.Items.Add(firstName);
                        lastNameListBox.Items.Add(lastName);

                    }
                }

            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
               FileName = _fileName,
               InitialDirectory = Environment.CurrentDirectory
            };
            if (sfd.ShowDialog() == true)
            {
                string fileNameFromSFD = sfd.FileName;
                using (FileStream fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        for (int i = 0; i < firstNameListBox.Items.Count; i++)
                        {
                            ListBoxItem firstName = firstNameListBox.Items[i] as ListBoxItem;
                            ListBoxItem lastName = lastNameListBox.Items[i] as ListBoxItem;

                            sw.WriteLine($"{firstName.Content.ToString()};{lastName.Content.ToString()}");
                        }
                    }
                }
            }
        }
    }
}
