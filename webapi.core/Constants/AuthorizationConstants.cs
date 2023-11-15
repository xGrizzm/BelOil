using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.core.Constants
{
    /// <summary>
    /// Class with JWT authorization constants
    /// </summary>
    public static class AuthorizationConstants
    {
        private const string _secretKey = "cJvTu3zQc5l84RBG1Y7TlNt2mn6R3b";

        /// <summary>
        /// Issuer
        /// </summary>
        public const string Issuer = "BELOIL API";
        /// <summary>
        /// Audience
        /// </summary>
        public const string Audience = "BELOIL APP";

        /// <summary>
        /// Token lifetime in hours
        /// </summary>
        public const int TokenLifetime = 24;

        /// <summary>
        /// Returns symmetric secret key
        /// </summary>
        /// <returns>Symmetric Secter key</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
    }
}
