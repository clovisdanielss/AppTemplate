using AppTemplate.Application;

namespace AppTemplate.Tests
{
    public class PasswordHasherShould
    {
        [Fact]
        public void AcceptPassword()
        {
            var hash = PasswordHasher.GeneratePasswordHashString("123456");
            var result = PasswordHasher.VerifyPassword(hash, "123456");
            Assert.True(result);
        }
        [Fact]
        public void RejectPassword()
        {
            var hash = PasswordHasher.GeneratePasswordHashString("123456");
            var result = PasswordHasher.VerifyPassword(hash, "123455");
            Assert.False(result);
        }
    }
}