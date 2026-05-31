using SweebAppAPIs.Models;
using SweebAppAPIs.Services.Interfaces;
using SweebAppAPIs.Models.Responses;
using SweebAppAPIs.Data.Repositories.Interfaces;

namespace SweebAppAPIs.Services
{

    public class UserServices(IUserRepository repo) : IUserServices
    {
        private readonly IUserRepository _repo = repo;
        
        public async Task<Response> CreateUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new Response
                {
                    Status = "info",
                    Message = "Username can't be empty or contain white spaces."
                };
            }

            if(username.Length < 4)
            {
                return new Response
                {
                    Status = "info",
                    Message = "Username must be at least 4 characters long."
                };
            }

            var user = new User
            {
                UserName = username
            };

            var result = await _repo.CreateUserAsync(user);
            if(result == null)
            {
                return new Response
                {
                    Status = "error",
                    Message = "Failed to create user."
                };
            }

            return new Response
            {
                Status = "success",
                Message = "User created successfully.",
                Data = result
            };
        }
        
        public async Task<Response> UpdateUserAsync(int userId, string newName)
        {
            if(userId == 0 || newName == null)
            {
                return new Response
                {
                    Status = "info",
                    Message = "Fields can't be empty."
                };
            }

            if(string.IsNullOrWhiteSpace(newName))
            {
                return new Response
                {
                    Status = "info",
                    Message = "Username can't be empty or contain white spaces."
                };
            }

            if(newName.Length < 4)
            {
                return new Response
                {
                    Status = "info",
                    Message = "Username must be at least 4 characters long."
                };
            }

            await _repo.UpdateUserAsync(userId, newName);

            return new Response
            {
                Status = "success",
                Message = "User updated successfully."
            };
        }

        public async Task<Response> DeleteUser(int userId)
        {
            if (userId == 0)
            {
                return new Response
                {
                    Status = "error",
                    Message = "User ID can't be empty."
                };
            }

            await _repo.DeleteUserAsync(userId);
            return new Response
            {
                Status = "info",
                Message = "User deleted successfully."
            };
        }

        public async Task<Response> GetUserAsync()
        {
            var user = await _repo.GetUserAsync();

            if(user == null)
            {
                return new Response
                {
                    Status = "Info",
                    Message = "No user found."
                };
            }

            return new Response
            {
                Status = "success",
                Message = "User retrieved successfully.",
                Data = user
            };
        }
    }
}
