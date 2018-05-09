using System;
using System.Windows.Input;
using Doodler.Implementation;
using Doodler.Models;

namespace Doodler.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Properties
        private string _email = string.Empty;
        private string _password = string.Empty;

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        public ICommand LoginCommand { get;  }
        public LoginModel Model { get; }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
            Model = new LoginModel();
        }

        private async void LoginAction(object o)
        {
            await Model.TryLoginAsync(Email, Password);
        }
    }
}
