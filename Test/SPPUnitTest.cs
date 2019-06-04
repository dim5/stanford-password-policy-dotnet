using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using StanfordPasswordPolicy;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class SPPUnitTest
    {
        private readonly StanfordPasswordValidator<PocoUser> validator = new StanfordPasswordValidator<PocoUser>();
        private readonly UserManager<PocoUser> manager = MockHelpers.MockUserManager<PocoUser>().Object;
        private readonly PocoUser user = new PocoUser();

        [Theory]
        [InlineData(@"Dvc^3a5n")]
        [InlineData(@"8pJ!VPcf*6J")]
        [InlineData(@"PGBf2yjie39e")]
        [InlineData(@"MMu%QpS2K%k4rm")]
        [InlineData(@"I266tSipiqWgZuE")]
        [InlineData(@"tPyRdGLAFYWHbEsF")]
        [InlineData(@"iAWEXWvRPWxcjMe65")]
        [InlineData(@"FrzRHrdQyZvwVnHxIBF")]
        [InlineData(@"zdyuufickqeuskshyyzo")]
        [InlineData(@"faster purposely underuse")]
        [InlineData(@"judo-corsage-prototype")]
        public async Task TestCorrectAsync(string password)
        {
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }

        [Fact]
        public async Task TestLengthAsync()
        {
            var errorCode = StanfordPasswordValidator<PocoUser>.ErrorCode.ShortLength;
            var password = string.Empty;
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = "a";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = "1";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = "@";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = "aaa";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = new string('a', 8);
            IdentityResultAssert.IsFailureNot(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
        }

        [Fact]
        public async Task TestSymbolAsync()
        {
            var errorCode = StanfordPasswordValidator<PocoUser>.ErrorCode.NoSymbol;
            var password = "aaa";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = new string('a', 8);
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);

            password = "8yRqUwzNpfn";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);


            password = new string('a', 12);
            IdentityResultAssert.IsFailureNot(await validator.ValidateAsync(manager, user, password), errorCode);
        }

        [Fact]
        public async Task TestNumbersAsync()
        {
            var errorCode = StanfordPasswordValidator<PocoUser>.ErrorCode.NoNumber;
            var password = "aaa";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = new string('a', 12);
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);

            password = "wLuf&k^uZuikAwG";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);

            password = new string('a', 16);
            IdentityResultAssert.IsFailureNot(await validator.ValidateAsync(manager, user, password), errorCode);
        }

        [Fact]
        public async Task TestMixedAsync()
        {
            var errorCode = StanfordPasswordValidator<PocoUser>.ErrorCode.NoMixedCase;
            var password = "aaa";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password),
                                            errorCode);
            password = new string('a', 16);
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);

            password = "AQ6ZM*7PVJ3EC4#!PZ&";
            IdentityResultAssert.IsFailure(await validator.ValidateAsync(manager, user, password), errorCode);

            password = new string('a', 20);
            IdentityResultAssert.IsSuccess(await validator.ValidateAsync(manager, user, password));
        }
    }
}
