namespace Seniors.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel()
    {
        Title = "Настройки";
        IconViewModel = "Assets/Setting.png";
        IconViewModelSelected = "Assets/SettingSelected.png";
    }
}