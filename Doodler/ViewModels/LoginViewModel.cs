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
        public ICommand LoginCommand { get; }
        public LoginModel Model { get; }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginAction);
            Model = new LoginModel();
        }

        private async void LoginAction(object o)
        {
            IsViewEnabled = false;
            bool successful = await Model.TryLoginAsync(Email, Password);
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
