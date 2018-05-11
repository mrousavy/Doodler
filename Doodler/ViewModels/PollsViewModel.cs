using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Doodler.ViewModels
{
    public class PollsViewModel : ViewModelBase
    {
        #region Properties

        private int _tileColumns;

        private ObservableCollection<Poll> _polls;

        private bool _isViewEnabled = true;

        private bool _showErrorDialog;

        private ICommand _addCommand;

        private object _dialogViewModel;

        public object DialogViewModel
        {
            get => _dialogViewModel;
            set => Set(ref _dialogViewModel, value);
        }
        public ICommand AddCommand
        {
            get => _addCommand;
            set => Set(ref _addCommand, value);
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
            AddCommand = new RelayCommand(AddAction);
            Model = new PollsModel();
            Load();
        }

        private void AddAction(object o)
        {
            DialogViewModel = new AddPollViewModel();
            ShowErrorDialog = true;
        }

        private async void Load()
        {
            try
            {
                var list = await Model.GetAllPollsAsync();
                Polls = new ObservableCollection<Poll>(list);
            } catch (Exception ex)
            {
                DialogViewModel = new ErrorDialogViewModel(ex.Message);
                ShowErrorDialog = true;
            }
        }
    }
}
