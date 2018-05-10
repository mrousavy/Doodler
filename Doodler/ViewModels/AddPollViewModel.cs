using Doodler.Implementation;
using DoodlerCore;
using System;
using Doodler.Models;

namespace Doodler.ViewModels
{
    public class AddPollViewModel : ViewModelBase
    {
        private string _title;
        private User _user = Statics.CurrentUser;
        private PollType _type;
        private DateTime _endDate = DateTime.Now;

        #region Properties
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
            set => Set(ref _type, value);
        }
        #endregion

        public AddPollViewModel()
        {
            Model = new AddPollModel();
        }
    }
}
