using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;

namespace Doodler.ViewModels
{
    public class PollsViewModel : ViewModelBase
    {
        public PollsViewModel()
        {
            AddCommand = new RelayCommand(AddAction);
            OpenCommand = new RelayCommand<Poll>(OpenAction);
            Model = new PollsModel();
            Load();
        }

        private async void OpenAction(Poll poll)
        {
            var vm = new PollViewModel {Poll = poll};
            await vm.LoadAsync();
            DialogViewModel = vm;
            ShowEmbedDialog = true;
        }

        private void AddAction(object o)
        {
            DialogViewModel = new AddPollViewModel();
            ShowEmbedDialog = true;
        }

        private async void Load()
        {
            try
            {
                UsersCount = await Model.GetUsersCountAsync();
                var list = await Model.GetAllPollsAsync();
                Polls = new ObservableCollection<Poll>(list);
            } catch (Exception ex)
            {
                DialogViewModel = new ErrorDialogViewModel(ex.Message);
                ShowEmbedDialog = true;
            }
        }

        #region Properties

        private int _tileColumns;

        private ObservableCollection<Poll> _polls;

        private bool _isViewEnabled = true;

        private bool _showEmbedDialog;

        private ICommand _addCommand;

        private object _dialogViewModel;

        private ICommand _openCommand;

        private int _usersCount;

        public int UsersCount
        {
            get => _usersCount;
            set => Set(ref _usersCount, value);
        }

        public ICommand OpenCommand
        {
            get => _openCommand;
            set => Set(ref _openCommand, value);
        }

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

        public bool ShowEmbedDialog
        {
            get => _showEmbedDialog;
            set
            {
                Set(ref _showEmbedDialog, value);
                if (!value)
                    Load(); // Reload when dialog closes
            }
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
    }
}