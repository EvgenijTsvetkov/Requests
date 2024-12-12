namespace Project
{
    public interface ICommand
    {
        bool IsCancellation { get; }
        
        void Execute();
        void Undo();
    }
}