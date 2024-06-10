using AppUsgph.Core;
using AppUsgph.DBLib.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BC = BCrypt.Net.BCrypt;

namespace AppUsgph.Wpf.Authentication.ViewModels
{

    /// <summary>
    /// VM de <see cref="AppUsgph.Wpf.Authentication.Views.MainView"/>
    /// </summary>
    internal class MainViewViewModel : ObservableObject
    {
        #region Attributes

        private TypeEvenement? _SelectedTypeEvenement;
        private Evenement? _SelectedEvenement;
        private User? _SelectedUser;

        private ObservableCollection<TypeEvenement> _TypeEvenements;
        private ObservableCollection<Evenement> _Evenements;
        private ObservableCollection<User> _Users;

        private DelegateCommand<object> _CmdAddTypeEvenement;
        private DelegateCommand<object> _CmdAddEvenement;
        private DelegateCommand<object> _CmdAddUser;

        private DelegateCommand<object> _CmdUpdateTypeEvenement;
        private DelegateCommand<object> _CmdUpdateEvenement;
        private DelegateCommand<object> _CmdUpdateUser;

        private DelegateCommand<object> _CmdDeleteTypeEvenement;
        private DelegateCommand<object> _CmdDeleteEvenement;
        private DelegateCommand<object> _CmdDeleteUser;

        private TypeEvenement _AddType;
        private Evenement _AddEvenement;
        private User _AddUser;

        private User? _UserIdCreation;

        public int? UserId { get; set; }

        #endregion

        #region Properties

        public ObservableCollection<TypeEvenement> TypeEvenements
        {
            get => _TypeEvenements;
            set => SetProperty(nameof(TypeEvenements), ref _TypeEvenements, value);
        }
        public ObservableCollection<Evenement> Evenements
        {
            get => _Evenements;
            set => SetProperty(nameof(Evenements), ref _Evenements, value);
        }
        public ObservableCollection<User> Users
        {
            get => _Users;
            set => SetProperty(nameof(Users), ref _Users, value);
        }
        public TypeEvenement? SelectedTypeEvenement
        {
            get => _SelectedTypeEvenement;
            set => SetProperty(nameof(SelectedTypeEvenement), ref _SelectedTypeEvenement, value);
        }
        public Evenement? SelectedEvenement
        {
            get => _SelectedEvenement;
            set => SetProperty(nameof(SelectedEvenement), ref _SelectedEvenement, value);
        }
        public User? SelectedUser
        {
            get => _SelectedUser;
            set => SetProperty(nameof(SelectedUser), ref _SelectedUser, value);
        }
        public DelegateCommand<object> CmdAddTypeEvenement
        {
            get => _CmdAddTypeEvenement;
            set => _CmdAddTypeEvenement = value;
        }
        public DelegateCommand<object> CmdAddEvenement
        {
            get => _CmdAddEvenement;
            set => _CmdAddEvenement = value;
        }
        public DelegateCommand<object> CmdAddUser
        {
            get => _CmdAddUser;
            set => _CmdAddUser = value;
        }
        public DelegateCommand<object> CmdUpdateTypeEvenement
        {
            get => _CmdUpdateTypeEvenement;
            set => _CmdUpdateTypeEvenement = value;
        }
        public DelegateCommand<object> CmdUpdateEvenement
        {
            get => _CmdUpdateEvenement;
            set => _CmdUpdateEvenement = value;
        }
        public DelegateCommand<object> CmdUpdateUser
        {
            get => _CmdUpdateUser;
            set => _CmdUpdateUser = value;
        }
        public DelegateCommand<object> CmdDeleteTypeEvenement
        {
            get => _CmdDeleteTypeEvenement;
            set => _CmdDeleteTypeEvenement = value;
        }
        public DelegateCommand<object> CmdDeleteEvenement
        {
            get => _CmdDeleteEvenement;
            set => _CmdDeleteEvenement = value;
        }
        public DelegateCommand<object> CmdDeleteUser
        {
            get => _CmdDeleteUser;
            set => _CmdDeleteUser = value;
        }

        public TypeEvenement AddType
        {
            get => _AddType;
            set => SetProperty(nameof(AddType), ref _AddType, value);
        }
        public Evenement AddEvenement
        {
            get => _AddEvenement;
            set => SetProperty(nameof(AddEvenement), ref _AddEvenement, value);
        }
        public User AddUser
        {
            get => _AddUser;
            set => SetProperty(nameof(AddUser), ref _AddUser, value);
        }

        public User UserIdCreation
        {
            get => _UserIdCreation;
            set => SetProperty(nameof(UserIdCreation), ref _UserIdCreation, value);
        }

        #endregion

        #region Constructors

        public MainViewViewModel()
        {
            using (AppUsgphContext mg = new())
            {
                TypeEvenements = new ObservableCollection<TypeEvenement>(mg.TypeEvenements.ToList());
            }
            CmdAddTypeEvenement = new DelegateCommand<object>(AddTypeEvenement, CanAddTypeEvenement)
                .ObservesProperty(() => this.AddType);

            CmdUpdateTypeEvenement = new DelegateCommand<object>(UpdateTypeEvenement, CanUpdateTypeEvenement)
                .ObservesProperty(() => this.SelectedTypeEvenement);

            CmdDeleteTypeEvenement = new DelegateCommand<object>(DeleteTypeEvenement, CanDeleteTypeEvenement)
                .ObservesProperty(() => this.SelectedTypeEvenement);

            

            using (AppUsgphContext context = new AppUsgphContext())
            {
                this.TypeEvenements = new ObservableCollection<TypeEvenement>(context.TypeEvenements);
            }
            this.AddType = new TypeEvenement();

            CmdAddEvenement = new DelegateCommand<object>(AddEvent, CanAddEvenement)
                .ObservesProperty(() => this.AddEvenement)
                .ObservesProperty(() => this.SelectedTypeEvenement);

            CmdUpdateEvenement = new DelegateCommand<object>(UpdateEvenement, CanUpdateEvenement)
                .ObservesProperty(() => this.SelectedEvenement);

            CmdDeleteEvenement = new DelegateCommand<object>(DeleteEvenement, CanDeleteEvenement)
                .ObservesProperty(() => this.SelectedEvenement);


            using (AppUsgphContext context = new AppUsgphContext())
            {
                this.Evenements = new ObservableCollection<Evenement>(context.Evenements);
            }
            this.AddEvenement = new Evenement();

            CmdAddUser = new DelegateCommand<object>(AddUtilisateur, CanAddUser)
                .ObservesProperty(() => this.AddUser)
                .ObservesProperty(() => this.SelectedUser);

            CmdUpdateUser = new DelegateCommand<object>(UpdateUser, CanUpdateUser)
                .ObservesProperty(() => this.SelectedUser);

            CmdDeleteUser = new DelegateCommand<object>(DeleteUser, CanDeleteUser)
                .ObservesProperty(() => this.SelectedUser);


            using (AppUsgphContext context = new AppUsgphContext())
            {
                this.Users = new ObservableCollection<User>(context.Users);
            }
            this.AddUser = new User();
        }




        #endregion

        #region Commands

        private void AddTypeEvenement(object parameter = null)
        {

            AddType.CreatedAt = DateTime.Now;
            UserId = 1;
            if (UserId != null)
            {
                using (AppUsgphContext context = new())
                    UserIdCreation = context.Users.FirstOrDefault(userTemp => userTemp.Id.Equals((int)UserId));
                if (UserIdCreation != null)
                {
                    AddType.UserIdCreate = UserIdCreation.Id;
                }
            }
            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.TypeEvenements.Add(AddType);
                context.SaveChanges();
            }
            this.TypeEvenements.Add(AddType);

            this.AddType = new TypeEvenement();
        }

        private void AddEvent(object parameter = null)
        {

            AddEvenement.CreatedAt = DateTime.Now;
            UserId = 1;
            if (UserId != null)
            {
                using (AppUsgphContext context = new())
                    UserIdCreation = context.Users.FirstOrDefault(userTemp => userTemp.Id.Equals((int)UserId));
                if (UserIdCreation != null)
                {
                    if (SelectedTypeEvenement == null)
                        throw new Exception("Les éléments nécessaire ne sont pas sélectionnés");
                    AddEvenement.UserIdCreate = UserIdCreation.Id;
                    AddEvenement.TypeEvenementId = SelectedTypeEvenement.Id;
                }
            }
            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Evenements.Add(AddEvenement);
                Console.WriteLine(AddEvenement);
                context.SaveChanges();
            }
            this.Evenements.Add(AddEvenement);

            this.AddEvenement = new Evenement();


        }


        private void AddUtilisateur(object parameter = null)
        {
            
             AddUser.CreatedAt = DateTime.Now;
             AddUser.HashedPassword = BC.HashPassword(AddUser.HashedPassword);
           
            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Users.Add(AddUser);
                context.SaveChanges();
            }
            this.Users.Add(AddUser);

            this.AddUser = new User();


        }

        private void UpdateTypeEvenement(object? parameter = null)
        {

            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.TypeEvenements.Update(SelectedTypeEvenement);
                context.SaveChanges();
            }
        }
        private void UpdateEvenement(object? parameter = null)
        {
            if (SelectedEvenement?.Type == null)
                throw new Exception("Les éléments nécessaire ne sont pas sélectionnés");

            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Evenements.Update(SelectedEvenement);
                context.SaveChanges();
            }
        }
        private void UpdateUser(object? parameter = null)
        {
            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Users.Update(SelectedUser);
                context.SaveChanges();
            }
        }

        private bool CanUpdateTypeEvenement(object? parameter = null) =>
            this.SelectedTypeEvenement != null;

        private bool CanUpdateEvenement(object? parameter = null) =>
            this.SelectedEvenement != null
            && this.SelectedEvenement.Type != null;
        private bool CanUpdateUser(object? parameter = null) =>
            this.SelectedUser != null;
        private void DeleteTypeEvenement(object? parameter = null)
        {
            if (SelectedTypeEvenement == null)
                throw new Exception("Les éléments nécessaire ne sont pas sélectionnés");

            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.TypeEvenements.Remove(SelectedTypeEvenement);
                context.SaveChanges();
            }
            this.TypeEvenements.Remove(SelectedTypeEvenement);
        }
        private void DeleteEvenement(object? parameter = null)
        {
            if (SelectedEvenement == null)
                throw new Exception("Les éléments nécessaire ne sont pas sélectionnés");

            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Evenements.Remove(SelectedEvenement);
                context.SaveChanges();
            }
            this.Evenements.Remove(SelectedEvenement);
        }
        private void DeleteUser(object? parameter = null)
        {
            if (SelectedUser == null)
                throw new Exception("Les éléments nécessaire ne sont pas sélectionnés");

            using (AppUsgphContext context = new AppUsgphContext())
            {
                context.Users.Remove(SelectedUser);
                context.SaveChanges();
            }
            this.Users.Remove(SelectedUser);
        }
        private bool CanDeleteTypeEvenement(object? parameter = null) =>
            this.SelectedTypeEvenement != null;
        private bool CanDeleteEvenement(object? parameter = null) =>
            this.SelectedEvenement != null;
        private bool CanDeleteUser(object? parameter = null) =>
            this.SelectedUser != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected new void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //TODO : Remettre les propriétés qu'on vérifie
        private bool CanAddTypeEvenement(object? parameter = null) =>
            this.AddType != null;
        private bool CanAddEvenement(object? parameter = null) =>
            this.AddEvenement != null;
        private bool CanAddUser(object? parameter = null) =>
            this.AddUser != null;

        private void ReloadContext()
        {
            this.TypeEvenements.Clear();
            using (AppUsgphContext context = new AppUsgphContext())
            {
                //context.OffreCastings.Add(new OffreCasting("Nouvelle offre de casting"));
                //this._Brands context.Brands.ToList();
                context.SaveChanges();
            }
        }
        

        #endregion
    }
}
