using System.Windows.Input;
using Doodler.Implementation;
using Doodler.Models;

namespace Doodler.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Properties
        private string _email;
        private string _username;
        private string _password;

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
            await Model.TryRegisterAsync(Email, Username, Password);
        }
    }
}
