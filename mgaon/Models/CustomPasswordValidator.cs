using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mgaon.Models
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public int RequiredLenght { get; set; }
        public CustomPasswordValidator(int lenght)
        {
            RequiredLenght = lenght;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (String.IsNullOrEmpty(password) || password.Length < RequiredLenght)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Минимальная длина пароля {RequiredLenght}"
                });
            }
            
            string pattern = "^[0-9]+$";

            if (!Regex.IsMatch(password, pattern))
            {
                errors.Add(new IdentityError { Description="Пароль должен состоять только из цифр" });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
