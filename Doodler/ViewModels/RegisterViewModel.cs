﻿using System.Windows.Input;
using Doodler.Implementation;
using Doodler.Models;

namespace Doodler.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Properties
        private string _email = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _isErrorDialogOpen;
        private bool _isViewEnabled = true;

        public bool IsViewEnabled
        {
            get => _isViewEnabled;
            set => Set(ref _isViewEnabled, value);
        }
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        public bool IsErrorDialogOpen
        {
            get => _isErrorDialogOpen;
            set => Set(ref _isErrorDialogOpen, value);
        }
        public ICommand RegisterCommand { get; }
        public RegisterModel Model { get; }
        #endregion

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterAction);
            Model = new RegisterModel();
        }

        private async void RegisterAction(object o)
        {
            IsViewEnabled = false;
            bool successful = await Model.TryRegisterAsync(Email, Username, Password);
            if (!successful)
            {
                IsErrorDialogOpen = true;
            } else
            {
                // TODO: Open Next View
            }
            IsViewEnabled = true;
        }
    }
}
