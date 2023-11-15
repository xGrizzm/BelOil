namespace webapi.core.Models.Responses
{
    /// <summary>
    /// Response class for the Login endpoint
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Contains information about user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// JWT token
        /// </summary>
        public string Token { get; set; }
    }
}
