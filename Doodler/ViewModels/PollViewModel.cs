using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;
using MaterialDesignThemes.Wpf;
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
        #region Properties

        private Poll _poll;
        private ICommand _voteCommand;
        private ObservableCollection<PollModel.AnswerWrapper> _answers;
        private bool _isViewEnabled = true;
        private int _transitionerIndex;
        private IList<Vote> _votes;

        private bool _canVote;

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
        public Poll Poll
        {
            get => _poll;
            set
            {
                Set(ref _poll, value);
                var _ = LoadAsync();
            }
        }
        public PollModel Model { get; set; }
        #endregion

        public PollViewModel()
        {
            VoteCommand = new RelayCommand(VoteAction);
            Model = new PollModel();
        }

        public async Task LoadAsync()
        {
            Votes = await Model.GetVotesAsync(Poll);
            var answers = await Model.GetAnswersAsync(Poll);
            Answers = new ObservableCollection<PollModel.AnswerWrapper>(answers
                .Select(a => new PollModel.AnswerWrapper(a,
                    Votes.Count(v => v.Answer.Id == a.Id)))
            );

            CanVote = Votes.Any(v => v.User.Id == Statics.CurrentUser.Id);
            TransitionerIndex = CanVote ? 0 : 1;
        }

        private async void VoteAction(object o)
        {
            IsViewEnabled = false;
            var selected = Answers.FirstOrDefault(a => a.Selected);
            if (selected != null)
            {
                await Model.VoteAsync(Poll, selected.Answer);
            }

            TransitionerIndex = 1;
            IsViewEnabled = true;
        }

        private void Close(IInputElement element)
        {
            if (DialogHost.CloseDialogCommand.CanExecute(null, element))
                DialogHost.CloseDialogCommand.Execute(null, element);
        }
    }
}
