using System.Threading;
using Project.Web;
using Zenject;

namespace Project
{
    public class GetWeatherForecast : ICommand
    {
        private IWebService _webService;
        
        private WeatherController _weatherController;
        
        private CancellationTokenSource _tokenSource;

        public bool IsCancellation => _tokenSource.IsCancellationRequested;

        [Inject]
        public void Construct(WeatherController weatherController)
        {
            _weatherController = weatherController;
        }
        
        public void Execute()
        {
            _tokenSource = new CancellationTokenSource();
            _weatherController.UpdateModelAsync(_tokenSource);
        }

        public void Undo()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}