using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Test
{
    /// <summary>
    /// Helper for tests to validate identity results.
    /// </summary>
    public static class IdentityResultAssert
    {
        /// <summary>
        /// Asserts that the result has Succeeded.
        /// </summary>
        /// <param name="result"></param>
        public static void IsSuccess(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        /// <summary>
        /// Asserts that the result has not Succeeded.
        /// </summary>
        public static void IsFailure(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
        }

        /// <summary>
        /// Asserts that the result has not Succeeded and that error is the first Error's code.
        /// </summary>
        public static void IsFailure(IdentityResult result, string code)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.Contains(result.Errors, e => e.Code == code);
        }

        /// <summary>
        /// Asserts that the result has not Succeeded and that error is the first Error's code.
        /// </summary>
        public static void IsFailureNot(IdentityResult result, string code)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.DoesNotContain(result.Errors, e => e.Code == code);
        }
    }
}