namespace Air;

public abstract class ViewModel<TUserControl> : CommandModel, IViewModel
    where TUserControl : View, new()
{
    TUserControl? _view;
    public View CreateView()
    {
        if (_view is not null)
        {
            return _view;
        }

        _view = new()
        {
            DataContext = this
        };

        OnInitialized(_view);

        return _view;
    }

    void IViewModel.InnerOnInitialized(View view)
    {
        OnInitialized((TUserControl)view);
    }

    protected virtual void OnInitialized(TUserControl view)
    {

    }
}
