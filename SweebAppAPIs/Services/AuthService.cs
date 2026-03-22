using SweebAppAPIs.Data.Repositories;
using SweebAppAPIs.Models;
using SweebAppAPIs.Services.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SweebAppAPIs.Services
{
    public class AuthService(IUserRepository userRepository, IPasswordHashService passwordHashService, IJwtService jwtService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHashService _passwordHashService = passwordHashService;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<Result> RegisterAsync(string username , string email , string password)
        {
            bool checkEmpty = String.IsNullOrEmpty(username) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password);
            bool checkUsernameLength = username.Length < 7;
            bool checkValidEmail = IsValidEmail(email);
            bool checkValidPassword = IsValidPassword(password);

            if (checkEmpty)
            {
                return new Result { 
                    Success = false,
                    Message = "All fields need to be filled."
                };
            }else if (checkUsernameLength)
            {
                return new Result
                {
                    Success = false,
                    Message = "Username needs to be at least 7 characters."
                };
            }else if (!checkValidEmail)
            {
                return new Result
                {
                    Success = false,
                    Message = "Email is not valid."
                };
            }else if (!checkValidPassword)
            {
                return new Result
                {
                    Success = false,
                    Message = "Password must be stronger."
                };
            }

            try
            {
                var hashedPassword = _passwordHashService.HashPassword(password);

                var response = await _userRepository.RegisterAsync(username, email, hashedPassword);

                if (!response)
                {
                    return new Result
                    {
                        Success = false,
                        Message = "User could not be registered."
                    };
                }

                return new Result
                {
                    Success = true,
                    Message = "Registered successfully!"
                };
            }
            catch
            {
                return new Result
                {
                    Success = false,
                    Message = "An unexpected error occurred."
                };
            }
        }

        public async Task<Result> LoginAsync(string username, string password)
        {
            bool checkEmpty = String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password);

            if (checkEmpty)
            {
                return new Result
                {
                    Success = false,
                    Message = "All fields need to be filled."
                };
            }
            try
            {
                var hashPassword = await _userRepository.LoginAsync(username);

                if(hashPassword?.PasswordHash == null)
                {
                    return new Result
                    {
                        Success = false,
                        Message = "Login faild."
                    };
                }

                bool verified = _passwordHashService.VerifyPassword(password, hashPassword.PasswordHash);

                if (!verified)
                {
                    return new Result
                    {
                        Success = false,
                        Message = "Password is incorect."
                    };
                }
                var userData = await _userRepository.GetUserData(username);

                if(userData == null)
                {
                   return new Result
                   {
                      Success = false,
                      Message = "Login faild."
                   };
                }
                    
                var token = _jwtService.GenerateToken(userData);

                return new Result
                {
                  Success = true,
                  Message = token
                };
            }
            catch
            {
                return new Result
                {
                    Success = false,
                    Message = "An unexpected error occurred."
                };
            }
        }


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
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
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
