using SweebAppAPIs.Data.Repositories;
using SweebAppAPIs.Services.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SweebAppAPIs.Services
{
    public class AuthService : IAuthService , IUserRepository , IPasswordHashService 
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(IAuthService authService , IUserRepository userRepository , IPasswordHashService passwordHashService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }
        /*
        public async string RegisterAsync(string username , string email , string password)
        {
            if (username.Length < 7)
            {
                return "Username need to be more than 7 characters.";
            }
            else if (!IsValidEmail(email))
            {
                return "Please insert an valid email.";
            }
            else if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(username))
            {
                return "Fields can't be empty.";
            }
            else if (!IsValidPassword(password))
            {
                return "At least 8 characters long.\r\nContains at least one lowercase letter.\r\nContains at least one uppercase letter.\r\nContains at least one digit.\r\nContains at least one special character from a predefined set (e.g., @$!%*?&).";
            }

            try
            {
                var hashedPassword = _passwordHashService.HashPassword(password);

                await _userRepository.RegisterAsync(username, email, hashedPassword);

                return "User register successfuly.";
            }
            catch (Exception ex)
            {
                    throw new Exception(ex.Message);
            }
        }
        */
        private static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&^#()[\]{}|\\/\-+_.:;=,~`])[^\s<>]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
