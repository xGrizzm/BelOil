namespace webapi.core.Models.Requests
{
    /// <summary>
    /// Class for the login model
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Login field
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password field
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public LoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
