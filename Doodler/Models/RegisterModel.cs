﻿using DoodlerCore.Exceptions;
using System.Threading.Tasks;

namespace Doodler.Models
{
    /// <summary>
    ///     mrousavy: Model for registering a new user
    /// </summary>
    public class RegisterModel
    {
        public async Task<bool> TryRegisterAsync(string email, string username, string password)
        {
            using (var service = Statics.NewService())
            {
                try
                {
                    await Statics.CreateDatabaseAsync(service);
                    Statics.CurrentUser = await service.RegisterAsync(email, username, password);
                    await service.SaveAsync();
                    return true;
                } catch (EmailExistsException)
                {
                    return false;
                }
            }
        }
    }
}