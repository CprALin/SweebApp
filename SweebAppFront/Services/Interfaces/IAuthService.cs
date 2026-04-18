using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services.Interfaces
{
    public interface IAuthService
    {
       Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
