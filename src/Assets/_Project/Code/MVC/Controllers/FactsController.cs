using System;
using System.Collections.Generic;
using System.Threading;
using Project.Ui;
using UniRx;
using Zenject;

namespace Project
{
    public class FactsController : IInitializable, IDisposable
    {
        private ICommandInvoker _commandInvoker;
        private FactsModelFactory _modelFactory;
        private CommandFactory _commandFactory;
        private IViewsProvider _viewsProvider;

        private FactsModel _model;
        private FactsView _view;
        
        private CancellationTokenSource _tokenSource;
        
        private List<IDisposable> _disposables = new List<IDisposable>();

        [Inject]
        public void Construct(ICommandInvoker commandInvoker, FactsModelFactory modelFactory,
            CommandFactory commandFactory, IViewsProvider viewsProvider)
        {
            _commandInvoker = commandInvoker;
            _modelFactory = modelFactory;
            _commandFactory = commandFactory;
            _viewsProvider = viewsProvider;
        }
        
        public void Initialize()
        {
            _model = _modelFactory.Create();
            _view = (FactsView) _viewsProvider.GetView<FactsView>();

            _model.Breeds.Subscribe(breeds => { _view.UpdateDisplay(breeds);}).AddTo(_disposables);
            _model.SelectedBreed.Subscribe(breed => { _view.ShowBreedInformation(breed);}).AddTo(_disposables);

            _view.OnRequestGetBreedData += OnRequestGetBreedDataHandler;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables) 
                disposable.Dispose();
            
            _disposables.Clear();
        }

        public void ShowView()
        {
            _view.Show();
        }

        public void CreateUpdateFactsCommand()
        {
            _commandInvoker.AddCommand(_commandFactory.Create(CommandType.UpdateFacts));
        }

        public void HideView()
        {
            _view.Hide();
            
            _commandInvoker.RemoveLastCommand();
        }

        public void UpdateModelAsync(CancellationTokenSource tokenSource)
        {
            _model.UpdateBreeds(tokenSource);
        }

        public void GetBreedDataAsync(string breedId, CancellationTokenSource tokenSource)
        {
            _model.GetBreedData(breedId, tokenSource);
        }

        private void OnRequestGetBreedDataHandler(string breedId)
        {
            var command = (GetBreedData) _commandFactory.Create(CommandType.GetBreedData);
            command.BreedId = breedId;
            
            _commandInvoker.RemoveLastCommand();
            _commandInvoker.AddCommand(command);
        }
    }
}