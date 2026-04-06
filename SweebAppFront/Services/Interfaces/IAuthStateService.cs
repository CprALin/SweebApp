using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services.Interfaces
{
    public interface IAuthStateService
    {
        bool IsLoggedIn { get; set; }
        string Username { get; set; }
    }
}
