using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
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
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password)
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
                Password = password,
                Inbox = new Inbox()
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

        public Task<IList<User>> GetAllUsersForPollAsync(Guid pollId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllUsersForPollAsync(Poll poll)
        {
            throw new NotImplementedException();
        }

        public Task<Poll> CreatePollAsync<TAnswer>(string title, PollType type, IEnumerable<TAnswer> answers) where TAnswer : Answer
        {
            throw new NotImplementedException();
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

        public Task<IList<Poll>> GetAllPollsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Poll>> GetAllPollsForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Poll>> GetAllPollsForUserAsync(User user)
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
