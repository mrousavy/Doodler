using Doodler.Implementation;
using DoodlerCore;

namespace Doodler.ViewModels
{
    public class PollViewModel : ViewModelBase
    {
        #region Properties

        private Poll _poll;

        public Poll Poll
        {
            get => _poll;
            set => Set(ref _poll, value);
        }
        #endregion
    }
}
