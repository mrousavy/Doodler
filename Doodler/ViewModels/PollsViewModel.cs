using System;
using Doodler.Implementation;
using DoodlerCore;
using System.Collections.ObjectModel;
using Doodler.Models;

namespace Doodler.ViewModels
{
    public class PollsViewModel : ViewModelBase
    {
        #region Properties

        private int _tileColumns;

        private ObservableCollection<Poll> _polls;

        private bool _isViewEnabled;

        private bool _showErrorDialog;

        private string _errorDialogMessage;

        public string ErrorDialogMessage
        {
            get => _errorDialogMessage;
            set => Set(ref _errorDialogMessage, value);
        }
        public bool ShowErrorDialog
        {
            get => _showErrorDialog;
            set => Set(ref _showErrorDialog, value);
        }
        public bool IsViewEnabled
        {
            get => _isViewEnabled;
            set => Set(ref _isViewEnabled, value);
        }
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
        public PollsModel Model { get; }
        #endregion

        public PollsViewModel()
        {
            Model = new PollsModel();
            Load();
        }

        private async void Load()
        {
            try
            {
                var list = await Model.GetAllPollsAsync();
                Polls = new ObservableCollection<Poll>(list);
            } catch (Exception ex)
            {
                ErrorDialogMessage = ex.Message;
                ShowErrorDialog = true;
            }
        }
    }
}
