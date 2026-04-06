using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services.Interfaces
{
    public interface INavigationService
    {
        View GetView(string key);
    }
}
