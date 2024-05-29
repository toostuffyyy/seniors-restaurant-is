using Avalonia.Controls.Shapes;
using Avalonia.Styling;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Seniors.Context;
using Seniors.Models;
using Seniors.Views;
using Splat;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Seniors.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private EmployeeRest EmployeeRests { get; set; }
        public List<ViewModelBase> MainMenuViewModels { get; set; }
        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set => this.RaiseAndSetIfChanged(ref _selectedViewModel, value);
        }
        
        public MainWindowViewModel()
        {
            MainMenuViewModels = new List<ViewModelBase>()
            {
                new ListDishViewModel(),
                new SettingsViewModel()
            };

            EmployeeRests = Locator.Current.GetService<DataBaseContext>().EmployeeRests
                .Include(a => a.Post)
                .FirstOrDefault();
            SelectedViewModel = MainMenuViewModels[0];
        }
    }
}