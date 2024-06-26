﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AppUsgph.Core;
using AppUsgph.DBLib.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;

namespace AppUsgph.Wpf.Authentication.ViewModels
{
    /// <summary>
    /// VM de <see cref="AppUsgph.Wpf.Authentication.Views.LoginView"/>
    /// </summary>
    internal class LoginViewViewModel : ObservableObject
    {
        #region Fields

        /// <summary>
        /// Utilisateur
        /// </summary>
        private string? _Email;

        /// <summary>
        /// Mot de passe
        /// </summary>
        private string? _Password;

        /// <summary>
        /// Message
        /// </summary>
        private string? _Message;

        /// <summary>
        /// Indique si l'utilisateur est connecté ou non.
        /// </summary>
        private bool? _IsLoggedIn;

        /// <summary>
        /// Récupère l'Id de l'utilisateur connecté
        /// </summary>
        public int? _UserId;

        #endregion

        #region Properties

        /// <summary>
        /// Obtient ou défini <see cref="_UserName"/>
        /// </summary>
        public string? Email { get => _Email; set => _Email = value; }

        /// <summary>
        /// Obtient ou défini <see cref="_Password"/>
        /// </summary>
        public string? Password { get => _Password; set => _Password = value; }

        /// <summary>
        /// Obtient ou défini <see cref="_Message"/>
        /// Vu qu'on veut actualisé la vue lors du changement, on utilise <see cref="ObservableObject.SetProperty{T}(string, ref T, T, bool)"/>
        /// </summary>
        public string? Message { get => _Message; set => SetProperty(nameof(Message), ref _Message, value); }
        
        /// <summary>
        /// Obtient ou défini <see cref="_IsLoggedIn"/>
        /// </summary>
        public bool? IsLoggedIn { get => _IsLoggedIn; private set => SetProperty(nameof(IsLoggedIn), ref _IsLoggedIn, value); }

        public int? UserId { get => _UserId; private set => SetProperty(nameof(UserId), ref _UserId, value); } 
        #endregion


        #region Constructors

        /// <summary>
        ///  Constructeur
        /// </summary>
        public LoginViewViewModel()
        {
            IsLoggedIn = false;
            UserId = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Méthode de d'authentification d'un utilisateur
        /// </summary>
        internal void Authenticate()
        {
            // Outil de hashage
            bool isPasswordValid = false;

            // L'utilisateur récupéré
            User? user = null;

            // On recherche l'utilisateur par son email
            using (AppUsgphContext context = new())
                user = context.Users.FirstOrDefault(userTemp => userTemp.Email.Equals(Email));

            // Si il n'existe pas, on renvoie une erreur
            if (user == null)
                Message = "Impossible de trouver l'utilisateur";
            else
            {
                string hashedPassword = user.HashedPassword;
                isPasswordValid = BC.Verify(Password, hashedPassword);
                UserId = user.Id;
            }

            switch (isPasswordValid)
            {
                case false:
                    Message = "Mot de passe incorrect";
                    break;
                case true:
                    // On défini le logging à true. La vue observe cette propriété et va se cacher si IsLogging = true.
                    IsLoggedIn = true;

                    break;
            }
        }

        #endregion


    }
}
