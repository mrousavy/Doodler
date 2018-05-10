using Doodler.Implementation;

namespace Doodler.ViewModels
{
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
