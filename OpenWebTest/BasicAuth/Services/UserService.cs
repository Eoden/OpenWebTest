using Microsoft.Extensions.Logging;

namespace RestServices.BasicAuth.Services
{
    ///<Summary>
    /// User Interface for the basic authent
    ///</Summary>
    public interface IUserService
    {
        bool IsValidUser(string userName, string password);
    }
    ///<Summary>
    /// User Class for the basic authent
    ///</Summary>
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        ///<Summary>
        /// Constructor of UserService
        ///</Summary>
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        ///<Summary>
        /// Method who validate the user connected
        ///</Summary>
        public bool IsValidUser(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
    }
}
