using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Doodler.ViewModels
{
    public class PollViewModel : ViewModelBase
    {
        #region Properties

        private Poll _poll;
        private ICommand _voteCommand;
        private ObservableCollection<PollModel.AnswerWrapper> _answers;

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
                UpdateAnswers();
            }
        }
        #endregion

        public PollViewModel()
        {
            VoteCommand = new RelayCommand(VoteAction);
        }

        private void UpdateAnswers()
        {
            if (Poll != null)
                Answers = new ObservableCollection<PollModel.AnswerWrapper>(Poll.Answers.Select(a => new PollModel.AnswerWrapper(a)));
        }

        private void VoteAction(object o)
        {
            var selected = Answers.FirstOrDefault(a => a.Selected);
        }
    }
}
