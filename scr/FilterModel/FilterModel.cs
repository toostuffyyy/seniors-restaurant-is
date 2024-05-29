using ReactiveUI;

namespace Seniors.FilterModel;

public class FilterModel<T> : ReactiveObject
{
    private bool _isChecked;

    public bool IsChecked
    {
        get => _isChecked;
        set => this.RaiseAndSetIfChanged(ref _isChecked, value);
    }
    public T Value { get; set; }
}