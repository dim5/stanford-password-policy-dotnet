﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordPasswordPolicy
{
    public class StanfordPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public const string ShortLength = "ShortPassword";
        public const string NoSymbol = "NoSymbols";
        public const string NoNumber = "NoNumbers";
        public const string NoMixedCase = "NotMixedCase";

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var errors = new List<IdentityError>();

            if (password.Length < 8)
                errors.Add(new IdentityError
                {
                    Code = ShortLength,
                    Description = "Passwords must be at least 8 characters long."
                });

            if (password.Length < 12 && !CheckSymbol(password))
                errors.Add(new IdentityError
                {
                    Code = NoSymbol,
                    Description = "Passwords shorter than 12 characters must contain symbols."
                });

            if (password.Length < 16 && !CheckNumber(password))
                errors.Add(new IdentityError
                {
                    Code = NoNumber,
                    Description = "Passwords shorter than 16 characters must contain numbers."
                });

            if (password.Length < 20 && !CheckMixedCase(password))
                errors.Add(new IdentityError
                {
                    Code = NoMixedCase,
                    Description = "Passwords shorter than 20 characters must contain lower and upper case characters."
                });

            var res = errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
            return Task.FromResult(res);
        }

        private static bool CheckMixedCase(string password) =>
            password.Any(char.IsUpper) && password.Any(char.IsLower);

        private static bool CheckNumber(string password) => password.Any(char.IsNumber);

        private static bool CheckSymbol(string password) => !password.All(char.IsLetterOrDigit);
    }
}
