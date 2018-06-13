using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Doodler.ViewModels
{
    public class PollViewModel : ViewModelBase
    {
        public PollViewModel()
        {
            VoteCommand = new RelayCommand(VoteAction);
            CloseCommand = new RelayCommand(CloseAction);
            Model = new PollModel();
        }

        private void CloseAction(object o)
        {
            if (o is IInputElement element)
                if (DialogHost.CloseDialogCommand.CanExecute(null, element))
                    DialogHost.CloseDialogCommand.Execute(null, element);
        }

        public async Task LoadAsync()
        {
            if (Poll == null)
                return;

            IList<Answer> answers;
            using (var service = Statics.NewService())
            {
                Votes = await service.GetVotesForPollAsync(Poll);
                answers = await service.GetAnswersForPollAsync(Poll);
                UsersCount = await service.GetUsersCountAsync();
            }

            Answers = new ObservableCollection<PollModel.AnswerWrapper>(answers
                .Select(a => new PollModel.AnswerWrapper(a,
                    Votes.Count(v => v.Answer.Id == a.Id),
                    Votes.Any(v => v.Answer.Id == a.Id && v.User.Id == Statics.CurrentUser.Id)))
            );

            CanVote = Votes.All(v => v.User.Id != Statics.CurrentUser.Id);
            TransitionerIndex = CanVote ? 0 : 1;
        }

        private async void VoteAction(object o)
        {
            IsViewEnabled = false;
            try
            {
                var selected = Answers.FirstOrDefault(a => a.Selected);
                if (selected != null)
                {
                    await Model.VoteAsync(Poll, selected.Answer);
                    await LoadAsync(); // reload first so answers update

                    TransitionerIndex = 1;
                    CanVote = false;
                }
            } catch (Exception ex)
            {
                DialogViewModel = new ErrorDialogViewModel(ex.Message);
                IsDialogVisible = true;
            }

            IsViewEnabled = true;
        }

        #region Properties

        private Poll _poll;
        private ICommand _voteCommand;
        private ObservableCollection<PollModel.AnswerWrapper> _answers;
        private bool _isViewEnabled = true;
        private bool _isDialogVisible;
        private int _transitionerIndex;
        private IList<Vote> _votes;
        private bool _canVote;
        private ICommand _closeCommand;
        private int _usersCount;
        private object _dialogViewModel;

        public int UsersCount
        {
            get => _usersCount;
            set => Set(ref _usersCount, value);
        }

        public bool CanVote
        {
            get => _canVote;
            set => Set(ref _canVote, value);
        }

        public IList<Vote> Votes
        {
            get => _votes;
            set => Set(ref _votes, value);
        }

        public int TransitionerIndex
        {
            get => _transitionerIndex;
            set => Set(ref _transitionerIndex, value);
        }

        public bool IsViewEnabled
        {
            get => _isViewEnabled;
            set => Set(ref _isViewEnabled, value);
        }

        public bool IsDialogVisible
        {
            get => _isDialogVisible;
            set => Set(ref _isDialogVisible, value);
        }

        public object DialogViewModel
        {
            get => _dialogViewModel;
            set => Set(ref _dialogViewModel, value);
        }

        public ObservableCollection<PollModel.AnswerWrapper> Answers
        {
            get => _answers;
            set => Set(ref _answers, value);
        }

        public ICommand VoteCommand
        {
            get => _voteCommand;
            set => Set(ref _voteCommand, value);
        }

        public ICommand CloseCommand
        {
            get => _closeCommand;
            set => Set(ref _closeCommand, value);
        }

        public Poll Poll
        {
            get => _poll;
            set => Set(ref _poll, value);
        }

        public PollModel Model { get; set; }

        #endregion
    }
}