using AppUsgph.DBLib.Models;
using AppUsgph.Wpf.Authentication.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppUsgph.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private AppUsgphContext _context;
        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewViewModel();
            _context = new AppUsgphContext();

            // Charger les données TypeEvenement depuis la base de données
            List<TypeEvenement> typeEvenements = _context.TypeEvenements.ToList();
            List<Evenement> Evenements = _context.Evenements.ToList();
            List<User> Users = _context.Users.ToList();

            // Lier les données au DataGrid
            TypeEvenementsDataGrid.ItemsSource = typeEvenements;
            EvenementsDataGrid.ItemsSource = Evenements;
            UsersDataGrid.ItemsSource = Users;

        }
    }
}
