namespace Project
{
    public interface ICommandInvoker
    {
        void AddCommand(ICommand command);
        void RemoveLastCommand();
    }
}