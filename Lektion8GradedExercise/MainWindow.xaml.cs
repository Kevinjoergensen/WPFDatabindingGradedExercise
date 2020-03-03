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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lektion8GradedExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> users;
        public MainWindow()
        {
            InitializeComponent();
            users = new ObservableCollection<User>();
            User user = new User("k142k23", "Jenny", 32, 81.2);
            users.Add(user);
            userlist.DataContext = users;
            inputs.DataContext = users;

        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt|Word Files (*.doc)|*.doc";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

                // Open document 
                string filename = dlg.FileName;
                
            string statbartekst ="";
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        int range = 0;
                        string line = sr.ReadToEnd();
                        System.Console.WriteLine(line);
                        foreach (var l in line.Split('\n'))
                        {
                        // Read the stream to a string, and write the string to the console.
                        string[] splitter = l.Split(';');
                        Console.WriteLine(splitter);
                        string id = splitter[0];
                        string name = splitter[1];
                        int age = int.Parse(splitter[2]);
                        double score = double.Parse(splitter[3]);
                            if (id != null && name != null && age > 0 && score > 0.0)
                            {
                                User user = new User(id, name, age, score);
                                users.Add(user);
                                range++;
                                }
                            }

                        statbartekst = " (Loaded:" +  range + " users)";
                    }

                }
                catch (IOException er)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(er.Message);
                    statbartekst = " (The file could not be read)";
                }
                        DateTime localDate = DateTime.Now;
                StatBar.Content = "Last loaded: " + localDate.ToString() + statbartekst;
            }
        }

        //DataBinding current user
        




    }



   public  class User : INotifyPropertyChanged {
        string Id { get; set; }
        string Name { get; set; }
        int Age { get; set; }
        double Score { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public User(string id, string name, int age, double score)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Score = score;
        }
        public string name
        {
            set
            {
                Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
            get
            {
                return Name;
            }
        }
        public string id
        {
            set
            {
                Id = value;
                NotifyPropertyChanged(nameof(Id));
            }
            get
            {
                return Id;
            }
        }

        public int age
        {
            set
            {
                Age = value;
                NotifyPropertyChanged(nameof(Age));
            }
            get
            {
                return Age;
            }
        }
        public double score
        {
            set
            {
                Score = value;
                NotifyPropertyChanged(nameof(Score));
            }
            get
            {
                return Score;
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override String ToString()
        {
            return Name + " (" + Age + ")";
        }
    }
}
