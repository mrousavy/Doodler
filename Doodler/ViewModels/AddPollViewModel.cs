using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doodler.Implementation;
using Doodler.Models;
using DoodlerCore;

namespace Doodler.ViewModels
{
    public class AddPollViewModel : ViewModelBase
    {
        private ICommand _addAnswerCommand;
        private object _addingValue;
        private ObservableCollection<Answer> _answers;
        private RelayCommand _createCommand;
        private DateTime _endDate = DateTime.Now.AddDays(7);
        private RelayCommand _nextCommand;
        private RelayCommand _previousCommand;
        private ICommand _removeListItemCommand;
        private string _title;
        private int _transitionerIndex;
        private PollType _type;
        private User _user = Statics.CurrentUser;

        public AddPollViewModel()
        {
            Model = new AddPollModel();
            Answers = new ObservableCollection<Answer>();
            RemoveListItemCommand = new RelayCommand<Answer>(RemoveListItemAction);
            AddAnswerCommand = new RelayCommand(AddAnswerAction);
            PreviousCommand = new RelayCommand(PreviousAction, o => TransitionerIndex > 0 && TransitionerIndex < 3);
            NextCommand = new RelayCommand(NextAction, o => TransitionerIndex < 2);
            CreateCommand = new RelayCommand(CreateAction, o => ValidatePoll());
        }

        private async void CreateAction(object o)
        {
            TransitionerIndex++;
            await Task.Delay(1000);
            await Model.CreatePollAsync(User, Title, EndDate, Answers);
            TransitionerIndex++;
        }

        private void PreviousAction(object o)
        {
            TransitionerIndex--;
            PreviousCommand.RaiseCanExecuteChanged();
            NextCommand.RaiseCanExecuteChanged();
            CreateCommand.RaiseCanExecuteChanged();
        }

        private void NextAction(object o)
        {
            TransitionerIndex++;
            PreviousCommand.RaiseCanExecuteChanged();
            NextCommand.RaiseCanExecuteChanged();
            CreateCommand.RaiseCanExecuteChanged();
        }

        private bool ValidatePoll() => !string.IsNullOrWhiteSpace(Title) &&
                                       EndDate > DateTime.Now &&
                                       Answers.Any() &&
                                       User != null;

        private void AddAnswerAction(object o)
        {
            if (o is string text)
                Answers.Add(new TextAnswer {Text = text});
            else if (o is DateTime date) Answers.Add(new DateAnswer {Date = date});
        }

        private void RemoveListItemAction(Answer answer)
        {
            Answers.Remove(answer);
        }


        #region Properties

        public object AddingValue
        {
            get => _addingValue;
            set => Set(ref _addingValue, value);
        }

        public RelayCommand CreateCommand
        {
            get => _createCommand;
            set => Set(ref _createCommand, value);
        }

        public RelayCommand NextCommand
        {
            get => _nextCommand;
            set => Set(ref _nextCommand, value);
        }

        public RelayCommand PreviousCommand
        {
            get => _previousCommand;
            set => Set(ref _previousCommand, value);
        }

        public int TransitionerIndex
        {
            get => _transitionerIndex;
            set => Set(ref _transitionerIndex, value);
        }

        public ICommand AddAnswerCommand
        {
            get => _addAnswerCommand;
            set => Set(ref _addAnswerCommand, value);
        }

        public ICommand RemoveListItemCommand
        {
            get => _removeListItemCommand;
            set => Set(ref _removeListItemCommand, value);
        }

        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => Set(ref _answers, value);
        }

        public AddPollModel Model { get; }

        public DateTime EndDate
        {
            get => _endDate;
            set => Set(ref _endDate, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        public PollType Type
        {
            get => _type;
            set
            {
                Set(ref _type, value);
                Answers.Clear();
            }
        }

        #endregion
    }
}