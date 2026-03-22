using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserData user);
    }
}
