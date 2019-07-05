using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StanfordPasswordPolicy
{
    public class StanfordPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public static class ErrorCode
        {
            public static readonly string ShortLength = "ShortPassword";
            public static readonly string NoSymbol = "NoSymbols";
            public static readonly string NoNumber = "NoNumbers";
            public static readonly string NoMixedCase = "NotMixedCase";
        }

        public static PasswordOptions NoDefaults =>
            new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 0,
                RequiredUniqueChars = 1,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            };

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var errors = new List<IdentityError>();

            if (password.Length < 8)
            {
                errors.Add(new IdentityError
                {
                    Code = ErrorCode.ShortLength,
                    Description = "Passwords must be at least 8 characters long."
                });
            }

            if (password.Length < 12 && !CheckSymbol(password))
            {
                errors.Add(new IdentityError
                {
                    Code = ErrorCode.NoSymbol,
                    Description = "Passwords shorter than 12 characters must contain symbols."
                });
            }

            if (password.Length < 16 && !CheckNumber(password))
            {
                errors.Add(new IdentityError
                {
                    Code = ErrorCode.NoNumber,
                    Description = "Passwords shorter than 16 characters must contain numbers."
                });
            }

            if (password.Length < 20 && !CheckMixedCase(password))
            {
                errors.Add(new IdentityError
                {
                    Code = ErrorCode.NoMixedCase,
                    Description = "Passwords shorter than 20 characters must contain lower and upper case characters."
                });
            }

            var res = errors.Any() ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
            return Task.FromResult(res);
        }

        private static bool CheckMixedCase(string password) =>
            password.Any(char.IsUpper) && password.Any(char.IsLower);

        private static bool CheckNumber(string password) => password.Any(char.IsNumber);

        private static bool CheckSymbol(string password) => !password.All(char.IsLetterOrDigit);
    }
}
