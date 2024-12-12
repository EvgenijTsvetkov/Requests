using System.Threading;
using Zenject;

namespace Project
{
    public class GetBreedData : ICommand
    {
        private FactsController _factsController;
        
        private CancellationTokenSource _tokenSource;

        public bool IsCancellation => _tokenSource.IsCancellationRequested;
        public string BreedId { get; set; }

        [Inject]
        public void Construct(FactsController controller)
        {
            _factsController = controller;
        }
        
        public void Execute()
        {
            _tokenSource = new CancellationTokenSource();
            _factsController.GetBreedDataAsync(BreedId, _tokenSource);
        }

        public void Undo()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}