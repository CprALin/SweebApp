using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserInfo user);
    }
}
