using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace usgph
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<User> Users { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            EventTypeComboBox.SelectedIndex = 0;

            // Initialiser la collection d'événements
            Events = new ObservableCollection<Event>();


            // Initialiser la collection d'utilisateurs
            Users = new ObservableCollection<User>();

            // Lier les collections aux DataGrids
            EventsDataGrid.ItemsSource = Events;
            UsersDataGrid.ItemsSource = Users;

            // Ajouter les placeholders initiaux
            AddPlaceholderText(EventNameTextBox, null);
            AddPlaceholderText(EventLocationTextBox, null);
            AddPlaceholderText(UserNameTextBox, null);
        }

        // Méthodes pour gérer les événements
        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            var newEvent = new Event
            {
                Id = Events.Count + 1,
                Name = EventNameTextBox.Text,
                Location = EventLocationTextBox.Text,
                Date = EventDatePicker.SelectedDate ?? DateTime.Now,
                Type = ((ComboBoxItem)EventTypeComboBox.SelectedItem)?.Content.ToString()
            };
            Events.Add(newEvent);
        }

        private void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventsDataGrid.SelectedItem is Event selectedEvent)
            {
                selectedEvent.Name = EventNameTextBox.Text;
                selectedEvent.Location = EventLocationTextBox.Text;
                selectedEvent.Date = EventDatePicker.SelectedDate ?? DateTime.Now;
                selectedEvent.Type = ((ComboBoxItem)EventTypeComboBox.SelectedItem)?.Content.ToString(); 

                EventsDataGrid.Items.Refresh();
            }
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventsDataGrid.SelectedItem is Event selectedEvent)
            {
                Events.Remove(selectedEvent);
            }
        }

        // Méthodes pour gérer les utilisateurs
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                Id = Users.Count + 1,
                Name = UserNameTextBox.Text,
                Password = UserPasswordBox.Password,
                Statut = ((ComboBoxItem)UserStatutComboBox.SelectedItem)?.Content.ToString()
            };
            Users.Add(newUser);
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.Name = UserNameTextBox.Text;
                selectedUser.Password = UserPasswordBox.Password;
                selectedUser.Statut = ((ComboBoxItem)UserStatutComboBox.SelectedItem)?.Content.ToString();
                UsersDataGrid.Items.Refresh();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                Users.Remove(selectedUser);
            }
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez ici la logique pour la déconnexion
            MessageBox.Show("Déconnexion réussie !");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Nom de l'événement" || textBox.Text == "Lieu" || textBox.Text == "Nom de l'utilisateur"))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == EventNameTextBox)
                {
                    textBox.Text = "Nom de l'événement";
                }
                else if (textBox == EventLocationTextBox)
                {
                    textBox.Text = "Lieu";
                }
                else if (textBox == UserNameTextBox)
                {
                    textBox.Text = "Nom de l'utilisateur";
                }
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}