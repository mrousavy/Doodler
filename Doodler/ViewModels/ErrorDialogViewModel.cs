using Doodler.Implementation;

namespace Doodler.ViewModels
{
    public class ErrorDialogViewModel : ViewModelBase
    {
        #region Properties
        private string _message;

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        } 
        #endregion

        public ErrorDialogViewModel(string message)
        {
            Message = message;
        }
    }
}
