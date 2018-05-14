using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using DoodlerCore.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DoodlerCore
{
    public class DataService : IDataService
    {
        public DataService(string database, string server, string username, string password)
        {
            ConnectionString = BuildConnectionString(database, server, username, password);
            Context = new DoodlerContext(ConnectionString);
            // TODO: Context.Configuration.LazyLoadingEnabled = false;
        }

        private DoodlerContext Context { get; }

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

            var user = new User(email, name, password);
            Context.Users.Add(user);
            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            var original = await Context.Users.FindAsync(user.Id) ??
                           throw new IdNotFoundException($"The user {user.Username} could not be found!");
            Context.Users.Remove(original);
        }

        public async Task EditUserAsync(User user)
        {
            var original = await Context.Users.FindAsync(user.Id) ??
                           throw new IdNotFoundException($"The user {user.Username} could not be found!");
            Context.Entry(original).CurrentValues.SetValues(user);
        }

        public Task<User> GetUserByIdAsync(int id) => throw new NotImplementedException();

        public Task<IList<User>> GetAllUsersAsync() => throw new NotImplementedException();

        public async Task<int> GetUsersCountAsync() => await Context.Users.CountAsync();

        public Task<IList<User>> GetAllUsersForPollAsync(Guid pollId) => throw new NotImplementedException();

        public Task<IList<User>> GetAllUsersForPollAsync(Poll poll) => throw new NotImplementedException();

        public Task<Poll> CreatePollAsync<TAnswer>(User creator, string title, DateTime endDate,
            IEnumerable<TAnswer> answers) where TAnswer : Answer
        {
            var poll = new Poll(title, Context.Users.Find(creator.Id), endDate);
            Context.Polls.Add(poll);

            foreach (var answer in answers)
            {
                answer.Poll = poll;
                Context.Answers.Add(answer);
            }

            return Task.FromResult(poll);
        }

        public Task DeletePollAsync(Poll poll) => throw new NotImplementedException();

        public Task EditPollAsync(Poll poll) => throw new NotImplementedException();

        public Task<Poll> GetPollByIdAsync(Guid id) => throw new NotImplementedException();

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

        public async Task VoteOnPoll<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer
        {
            if (user == null)
                throw new ArgumentException(nameof(user));
            if (poll == null)
                throw new ArgumentException(nameof(poll));
            if (answer == null)
                throw new ArgumentException(nameof(answer));

            var originalUser = await Context.Users.FindAsync(user.Id);
            var originalPoll = await Context.Polls.FindAsync(poll.Id);
            var originalAnswer = await Context.Answers.FindAsync(answer.Id);
            var vote = new Vote(originalUser, originalPoll, originalAnswer);

            Context.Votes.Add(vote);
        }

        public Task RemoveVote<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer =>
            throw new NotImplementedException();

        public string ConnectionString { get; }
        public bool IsDirty => Context.ChangeTracker.HasChanges();
        public Task<int> SaveAsync() => Context.SaveChangesAsync();
        public int Save() => Context.SaveChanges();

        public Task EnsureCreatedAsync() => Context.Database.EnsureCreatedAsync();

        private static string BuildConnectionString(string database, string server, string username, string password) =>
            $"Server={server};Database={database};User={username};Password={password};Trusted_Connection=False;";
    }
}