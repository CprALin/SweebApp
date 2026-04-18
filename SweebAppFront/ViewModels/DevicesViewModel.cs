using SweebAppAPIs.Models;
using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        public ObservableCollection<Devices> Devices { get; set; }
        private int _columns = 1;
        public int Columns
        {
            get => _columns;
            set
            {
                if(_columns != value)
                {
                    _columns = value;
                    OnPropertyChanged();
                }
            }
        }
        public DevicesViewModel()
        {
            Devices = new ObservableCollection<Devices>
            {

                new Devices
                {
                    IdDevice = 1,
                    Name = "Laptop Dell XPS 13",
                    OS = "Windows 11",
                    CreatedAt = "2024-01-12 10:24",
                    UserId = 101
                },
                new Devices
                {
                    IdDevice = 2,
                    Name = "MacBook Pro 14",
                    OS = "macOS Sonoma",
                    CreatedAt = "2024-02-03 14:10",
                    UserId = 101
                },
                new Devices
                {
                    IdDevice = 3,
                    Name = "iPhone 15",
                    OS = "iOS 17",
                    CreatedAt = "2024-02-20 09:48",
                    UserId = 101
                }

            };
        }
    }
}
