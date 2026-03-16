using SweebAppAPIs.Data.Repositories;
using SweebAppAPIs.Models;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfo?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }
}
