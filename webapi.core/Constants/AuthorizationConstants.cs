using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.core.Constants
{
    /// <summary>
    /// Class with JWT authorization constants
    /// </summary>
    public static class AuthorizationConstants
    {
        private const string _secretKey = "jVm56wJtRIXDIGaYfAIAI199PYTzpL";

        /// <summary>
        /// Issuer
        /// </summary>
        public const string Issuer = "GKS WEB API";
        /// <summary>
        /// Audience
        /// </summary>
        public const string Audience = "GKS VUE APP";

        /// <summary>
        /// Token lifetime in hours
        /// </summary>
        public const int TokenLifetime = 12; 

        /// <summary>
        /// Returns symmetric secret key
        /// </summary>
        /// <returns>Symmetric Secter key</returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
    }
}
