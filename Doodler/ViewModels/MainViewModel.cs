using Doodler.Implementation;

namespace Doodler.ViewModels
{
    /// <summary>
    ///     mrousavy: ViewModel for the main page
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        private int _transitionerIndex;

        public int TransitionerIndex
        {
            get => _transitionerIndex;
            set => Set(ref _transitionerIndex, value);
        }

        #endregion
    }
}