using System.ComponentModel;

namespace SweebAppFront.Services.Interfaces
{
    public interface IAuthStateService : INotifyPropertyChanged
    {
        bool IsLoggedIn { get; set; }
        string Username { get; set; }
    }
}
