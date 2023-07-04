namespace Air;

public interface IViewModel
{
    View CreateView();
    internal void InnerOnInitialized(View view);
}