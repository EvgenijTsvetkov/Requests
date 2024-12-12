namespace Project.Ui
{
    public interface IViewsProvider
    {
        SimpleElement GetView<T>() where T: SimpleElement;
    }
}