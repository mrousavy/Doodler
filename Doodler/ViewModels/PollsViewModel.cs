using Doodler.Implementation;
using DoodlerCore;
using System.Collections.ObjectModel;

namespace Doodler.ViewModels
{
    public class PollsViewModel : ViewModelBase
    {
        #region Properties

        private int _tileColumns;

        private ObservableCollection<Poll> _polls;

        public ObservableCollection<Poll> Polls
        {
            get => _polls;
            set => Set(ref _polls, value);
        }
        public int TileColumns
        {
            get => _tileColumns;
            set => Set(ref _tileColumns, value);
        }
        #endregion
    }
}
