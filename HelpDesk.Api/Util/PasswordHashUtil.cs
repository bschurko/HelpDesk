using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Api.Util
{
    public static class PasswordHashUtil
    {
        private static readonly PasswordHasher<object> Hasher = new();

        public static string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            return Hasher.HashPassword(null!, password);
        }

        public static bool Verify(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(passwordHash))
                return false;

            var result = Hasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password
            );

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }

}
