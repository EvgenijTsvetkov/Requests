using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Ui;
using UniRx;
using Zenject;

namespace Project
{
    public class WeatherController : IInitializable, IDisposable
    {
        private ICommandInvoker _commandInvoker;
        private WeatherModelFactory _modelFactory;
        private CommandFactory _commandFactory;
        private IViewsProvider _viewsProvider;

        private WeatherModel _model;
        private WeatherView _view;
        
        private CancellationTokenSource _tokenSource;

        private List<IDisposable> _disposables = new List<IDisposable>();
      

        [Inject]
        public void Construct(ICommandInvoker commandInvoker, WeatherModelFactory modelFactory,
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
            _view = (WeatherView) _viewsProvider.GetView<WeatherView>();

            _model.Data.Subscribe(weatherData => { _view.UpdateDisplay(weatherData);}).AddTo(_disposables);
        }
        
        public void Dispose()
        {
            _tokenSource?.Dispose();
            
            foreach (var disposable in _disposables) 
                disposable.Dispose();
            
            _disposables.Clear();
        }

        public async UniTaskVoid ActivateLoopUpdate()
        {
            _view.Show();

            while (true)
            {
                _tokenSource = new CancellationTokenSource();
                
                await UniTask.WaitForSeconds(5, cancellationToken: _tokenSource.Token);

                if (_tokenSource.IsCancellationRequested)
                    break;

                _commandInvoker.AddCommand(_commandFactory.Create(CommandType.UpdateWeather));
            }
        }

        public void DeactivateLoopUpdate()
        {
            _view.Hide();
            
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();

            _commandInvoker.RemoveLastCommand();
        }

        public void UpdateModelAsync(CancellationTokenSource cancellationTokenSource)
        {
            _model.AsyncUpdate(cancellationTokenSource);
        }
    }
}