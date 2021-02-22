using Microsoft.AspNetCore.Identity;

namespace AspNetCoreNuxt.Extensions.Identity
{
    /// <inheritdoc cref="IStringHasher"/>
    public class StringHasher : IStringHasher
    {
        /// <summary>
        /// <see cref="PasswordHasher{TUser}"/> オブジェクトを表します。
        /// </summary>
        private static readonly PasswordHasher<object> PasswordHasher = new PasswordHasher<object>();

        /// <inheritdoc/>
        public string HashValue(string value)
            => PasswordHasher.HashPassword(user: null, value);

        /// <inheritdoc/>
        public bool VerifyHashedValue(string hashedValue, string providedValue)
            => hashedValue is not null
            && providedValue is not null
            && PasswordHasher.VerifyHashedPassword(user: null, hashedValue, providedValue)
            != PasswordVerificationResult.Failed;
    }
}
