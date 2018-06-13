using Doodler.Implementation;

namespace Doodler.ViewModels
{
    /// <summary>
    ///     mrousavy: ViewModel for an error dialog that gets automatically wired to a WPF Template
    /// </summary>
    public class ErrorDialogViewModel : ViewModelBase
    {
        public ErrorDialogViewModel(string message)
        {
            Message = message;
        }

        #region Properties

        private string _message;

        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        #endregion
    }
}