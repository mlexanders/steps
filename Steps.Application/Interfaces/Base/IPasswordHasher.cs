using Microsoft.AspNetCore.Identity;

namespace Steps.Application.Interfaces.Base;

public interface IPasswordHasher
{
    /// <summary>
    /// Returns a hashed representation of the supplied <paramref name="password"/> for the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user whose password is to be hashed.</param>
    /// <param name="password">The password to hash.</param>
    /// <returns>A hashed representation of the supplied <paramref name="password"/> for the specified <paramref name="user"/>.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Returns a <see cref="PasswordVerificationResult"/> indicating the result of a password hash comparison.
    /// </summary>
    /// <param name="user">The user whose password should be verified.</param>
    /// <param name="hashedPassword">The hash value for a user's stored password.</param>
    /// <param name="providedPassword">The password supplied for comparison.</param>
    /// <returns>A <see cref="PasswordVerificationResult"/> indicating the result of a password hash comparison.</returns>
    /// <remarks>Implementations of this method should be time consistent.</remarks>
    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}