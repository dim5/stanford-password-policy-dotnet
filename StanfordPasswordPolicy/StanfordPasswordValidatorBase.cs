using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace StanfordPasswordPolicy
{
    public abstract class StanfordPasswordValidatorBase
    {
        public static class ErrorCode
        {
            public static readonly string ShortLength = "ShortPassword";
            public static readonly string NoSymbol = "NoSymbols";
            public static readonly string NoNumber = "NoNumbers";
            public static readonly string NoMixedCase = "NotMixedCase";
        }

        /// <summary>
        /// A clear set of PasswordOptions, to reset Identity's defaults, since StanfordPasswordValidator ignores these options.
        /// </summary>
        public static PasswordOptions NoDefaultPasswordOptions =>
            new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 0,
                RequiredUniqueChars = 1,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            };

        protected static bool CheckMixedCase(string password) =>
            password.Any(char.IsUpper) && password.Any(char.IsLower);

        protected static bool CheckNumber(string password) => password.Any(char.IsNumber);

        protected static bool CheckSymbol(string password) => !password.All(char.IsLetterOrDigit);
    }
}