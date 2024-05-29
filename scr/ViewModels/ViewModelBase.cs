using ReactiveUI;

namespace Seniors.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {

        private string _title;
        private string _iconViewModel;
        private string _iconViewModelSelected;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string IconViewModel
        {
            get => _iconViewModel;
            set => this.RaiseAndSetIfChanged(ref _iconViewModel, value);
        }

        public string IconViewModelSelected
        {
            get => _iconViewModelSelected;
            set => this.RaiseAndSetIfChanged(ref _iconViewModelSelected, value);
        }
    }
}