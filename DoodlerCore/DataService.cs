using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace DoodlerCore
{
    public class DataService : IDataService
    {
        private DoodlerContainer Context { get; }

        private static string BuildConnectionString(string database, string server, string username, string password)
        {
            return "metadata=res://*/Doodler.csdl|res://*/Doodler.ssdl|res://*/Doodler.msl;" +
                   $"provider=System.Data.SqlClient;provider connection string=\"data source={server};" +
                   $"initial catalog={database};persist security info=True;user id={username};password={password};" +
                   "MultipleActiveResultSets=True;App=EntityFramework\"";
        }

        public DataService(string database, string server, string username, string password)
        {
            ConnectionString = BuildConnectionString(database, server, username, password);
            Context = new DoodlerContainer(ConnectionString);
            Context.Configuration.LazyLoadingEnabled = false;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await Context.Users
                       .FirstOrDefaultAsync(u => u.Email == email && u.Password == password)
                       ?? throw new InvalidCredentialException("Invalid Email or Password!");
        }

        public async Task<User> RegisterAsync(string email, string name, string password)
        {
            bool exists = await Context.Users.AnyAsync(u => u.Email == email);
            if (exists)
                throw new EmailExistsException(email);

            var user = new User
            {
                Email = email,
                Username = name,
                Password = password
            };
            Context.Users.Add(user);
            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            var original = await Context.Users.FindAsync(user.Id) ?? throw new ObjectNotFoundException($"The user {user.Username} could not be found!");
            Context.Users.Remove(original);
        }

        public async Task EditUserAsync(User user)
        {
            var original = await Context.Users.FindAsync(user.Id) ?? throw new ObjectNotFoundException($"The user {user.Username} could not be found!");
            Context.Entry(original).CurrentValues.SetValues(user);
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetUsersCountAsync() => await Context.Users.CountAsync();

        public Task<IList<User>> GetAllUsersForPollAsync(Guid pollId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllUsersForPollAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        public Task<Poll> CreatePollAsync<TAnswer>(User creator, string title, DateTime endDate, IEnumerable<TAnswer> answers) where TAnswer : Answer
        {
            Poll poll;
            if (typeof(TAnswer) == typeof(DateAnswer))
            {
                // Date Poll
                poll = new DatePoll
                {
                    Creator = creator,
                    CreatedAt = DateTime.Now,
                    EndsAt = endDate,
                    Title = title
                };
                Context.Polls.Add(poll);
            } else
            {
                // Text Poll
                poll = new TextPoll
                {
                    Creator = creator,
                    CreatedAt = DateTime.Now,
                    EndsAt = endDate,
                    Title = title
                };
                Context.Polls.Add(poll);
            }

            foreach (var answer in answers)
            {
                answer.Poll = poll;
                Context.Answers.Add(answer);
            }

            return Task.FromResult(poll);
        }

        public Task DeletePollAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        public Task EditPollAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        public Task<Poll> GetPollByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Poll>> GetAllPollsAsync() => await Context.Polls
            .Include(p => p.Creator)
            .ToListAsync();

        public async Task<IList<Poll>> GetAllPollsForUserAsync(int userId) => await Context.Polls
            .Include(p => p.Creator)
            .Where(p => p.Creator.Id == userId)
            .ToListAsync();

        public Task<IList<Poll>> GetAllPollsForUserAsync(User user) => GetAllPollsForUserAsync(user.Id);

        public async Task<IList<Vote>> GetVotesForPollAsync(Poll poll) => await Context.Votes
            .Include(v => v.Poll)
            .Include(v => v.Answer)
            .Include(v => v.User)
            .Where(v => v.Poll.Id == poll.Id)
            .ToListAsync();

        public async Task<IList<Answer>> GetAnswersForPollAsync(Poll poll) => await Context.Answers
            .Include(v => v.Poll)
            .Where(a => a.Poll.Id == poll.Id)
            .ToListAsync();

        public Task VoteOnPoll<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer
        {
            if (user == null)
                throw new ArgumentException(nameof(user));
            if (poll == null)
                throw new ArgumentException(nameof(poll));
            if (answer == null)
                throw new ArgumentException(nameof(answer));

            var vote = new Vote
            {
                Poll = Context.Polls.Find(poll.Id),
                Answer = Context.Answers.Find(answer.Id),
                User = Context.Users.Find(user.Id)
            };

            Context.Votes.Add(vote);
            return Task.CompletedTask;
        }

        public Task RemoveVote<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer
        {
            throw new NotImplementedException();
        }

        public string ConnectionString { get; }
        public bool IsDirty => Context.ChangeTracker.HasChanges();
        public Task<int> SaveAsync() => Context.SaveChangesAsync();
        public int Save() => Context.SaveChanges();
        public bool Exists()
        {
            return Context.Database.Exists();
        }

        public void Create()
        {
            Context.Database.CreateIfNotExists();
        }
    }
}
