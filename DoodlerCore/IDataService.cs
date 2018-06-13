using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DoodlerCore.Exceptions;

namespace DoodlerCore
{
    /// <summary>
    ///     Represents the type of a Poll
    /// </summary>
    public enum PollType
    {
        /// <summary>
        ///     A poll with Text as answers
        /// </summary>
        TextPoll,

        /// <summary>
        ///     A poll with Dates as answers
        /// </summary>
        DatePoll
    }

    /// <inheritdoc />
    /// <summary>
    ///     A database functions interface and service
    /// </summary>
    public interface IDataService : IDisposable
    {
        #region User

        /// <summary>
        ///     Login with the given email and password, if login fails this will throw an
        ///     <see cref="System.Security.Authentication.InvalidCredentialException" />
        /// </summary>
        /// <exception cref="System.Security.Authentication.InvalidCredentialException">
        ///     Thrown if the email or password is
        ///     incorrect
        /// </exception>
        /// <param name="email">The user's email address</param>
        /// <param name="password">The user's password</param>
        /// <returns>The <see cref="User" /> object on the database</returns>
        Task<User> LoginAsync([EmailAddress] string email, string password);

        /// <summary>
        ///     Register a new user on the database with the given email, username and password. If the user exists, this will
        ///     throw an <see cref="EmailExistsException" />
        /// </summary>
        /// <exception cref="EmailExistsException">Thrown if the given email is already registered</exception>
        /// <param name="email">The email to use</param>
        /// <param name="name">The username or display name</param>
        /// <param name="password">The user's password</param>
        /// <returns>The newly created <see cref="User" /> object</returns>
        Task<User> RegisterAsync([EmailAddress] string email, string name, string password);

        /// <summary>
        ///     Delete the given user from the database
        /// </summary>
        /// <param name="user">The user to delete</param>
        Task DeleteUserAsync(User user);

        /// <summary>
        ///     Edit the given user on the database
        /// </summary>
        /// <param name="user">The user to edit</param>
        Task EditUserAsync(User user);

        /// <summary>
        ///     Find a user by it's ID on the database
        /// </summary>
        /// <param name="id">The ID to search for</param>
        /// <returns>The found <see cref="User" /> object</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        ///     Find all users registered on the database
        /// </summary>
        /// <returns>The found <see cref="User" />s</returns>
        Task<IList<User>> GetAllUsersAsync();

        /// <summary>
        ///     Find the count of all users on this database
        /// </summary>
        /// <returns>The amount of found <see cref="User" />s</returns>
        Task<int> GetUsersCountAsync();

        /// <summary>
        ///     Find all users that voted on the given poll
        /// </summary>
        /// <param name="pollId">The poll's ID</param>
        /// <returns>The found <see cref="User" />s</returns>
        Task<IList<User>> GetAllUsersForPollAsync(Guid pollId);

        /// <summary>
        ///     Find all users that voted on the given poll
        /// </summary>
        /// <param name="poll">The poll</param>
        /// <returns>The found <see cref="User" />s</returns>
        Task<IList<User>> GetAllUsersForPollAsync(Poll poll);

        #endregion

        #region Poll

        /// <summary>
        ///     Create a new date or text poll on the database
        /// </summary>
        /// <typeparam name="TAnswer">The type of the Answers, must be compliant with the <see cref="PollType" /> parameter</typeparam>
        /// <param name="creator">The creator of this poll</param>
        /// <param name="title">The title of the poll shown to all users</param>
        /// <param name="endDate">The date this poll ends at</param>
        /// <param name="answers">The possible answers for this poll</param>
        /// <returns>The newly created <see cref="Poll" /> object</returns>
        Task<Poll> CreatePollAsync<TAnswer>(User creator, string title, DateTime endDate, IEnumerable<TAnswer> answers)
            where TAnswer : Answer;

        /// <summary>
        ///     Delete a given poll from the database
        /// </summary>
        /// <param name="poll">The poll to delete</param>
        Task DeletePollAsync(Poll poll);

        /// <summary>
        ///     Edit a given poll on the database
        /// </summary>
        /// <param name="poll">The poll to delete</param>
        Task EditPollAsync(Poll poll);

        /// <summary>
        ///     Find a <see cref="Poll" /> object with the given ID or throw a <see cref="PollNotFoundException" />
        /// </summary>
        /// <exception cref="PollNotFoundException">Thrown if the poll could not be found on the database</exception>
        /// <param name="id">The poll ID to search for</param>
        /// <returns>The found <see cref="Poll" /> object</returns>
        Task<Poll> GetPollByIdAsync(int id);

        /// <summary>
        ///     Find all polls created on the database
        /// </summary>
        /// <returns>The found <see cref="Poll" />s</returns>
        Task<IList<Poll>> GetAllPollsAsync();

        /// <summary>
        ///     Find all polls created by the given user
        /// </summary>
        /// <param name="userId">The poll creator's ID</param>
        /// <returns>The found <see cref="Poll" />s</returns>
        Task<IList<Poll>> GetAllPollsForUserAsync(int userId);

        /// <summary>
        ///     Find all polls created by the given user
        /// </summary>
        /// <param name="user">The poll creator</param>
        /// <returns>The found <see cref="Poll" />s</returns>
        Task<IList<Poll>> GetAllPollsForUserAsync(User user);

        /// <summary>
        ///     Find all votes submitted on the given poll
        /// </summary>
        /// <param name="poll">The poll</param>
        /// <returns>The found <see cref="Vote" />s</returns>
        Task<IList<Vote>> GetVotesForPollAsync(Poll poll);

        /// <summary>
        ///     Get all answers on the given poll
        /// </summary>
        /// <param name="poll">The poll</param>
        /// <returns>The found <see cref="Answer" />s</returns>
        Task<IList<Answer>> GetAnswersForPollAsync(Poll poll);

        /// <summary>
        ///     Vote on a poll
        /// </summary>
        /// <typeparam name="TAnswer">The type of the Answer (<see cref="DateAnswer" /> or <see cref="TextAnswer" />)</typeparam>
        /// <param name="user">The user that is voting on this poll</param>
        /// <param name="poll">The poll to vote on</param>
        /// <param name="answer">The selected answer of this poll</param>
        /// <exception cref="PollExpiredException">
        ///     Thrown if the Date of the Poll is expired.
        /// </exception>
        /// <returns></returns>
        Task VoteOnPoll<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer;

        /// <summary>
        ///     Remove a vote from a poll
        /// </summary>
        /// <typeparam name="TAnswer">The type of the Answer (<see cref="DateAnswer" /> or <see cref="TextAnswer" />)</typeparam>
        /// <param name="user">The user that is voting on this poll</param>
        /// <param name="poll">The poll to vote on</param>
        /// <param name="answer">The selected answer of this poll</param>
        /// <returns></returns>
        Task RemoveVote<TAnswer>(User user, Poll poll, TAnswer answer) where TAnswer : Answer;

        #endregion

        #region Vote
        /// <summary>
        ///     Delete the given vote async
        /// </summary>
        Task DeleteVoteAsync(Vote vote);
        #endregion

        #region Answer
        /// <summary>
        ///     Delete the given answer async
        /// </summary>
        Task DeleteAnswerAsync(Answer answer);
        #endregion

        #region Report
        /// <summary>
        ///     Report the given poll with a report description
        /// </summary>
        Task ReportPollAsync(Poll poll, String text);
        #endregion

        #region Data Service

        /// <summary>
        ///     Gets the currently used SQL connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///     Checks whether the database has pending changes
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        ///     Save all pending changes async
        /// </summary>
        /// <returns>The number of rows affected</returns>
        Task<int> SaveAsync();

        /// <summary>
        ///     Save all pending changes
        /// </summary>
        /// <returns>The number of rows affected</returns>
        int Save();

        /// <summary>
        ///     Ensures that the database is created
        /// </summary>
        Task EnsureCreatedAsync();

        #endregion
    }
}