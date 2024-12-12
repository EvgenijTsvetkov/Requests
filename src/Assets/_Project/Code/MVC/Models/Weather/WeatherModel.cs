using System.Threading;
using Project.Web;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class WeatherModel
    {
        private IWebService _webService;
        
        public ReactiveProperty<WeatherData> Data { get; }

        [Inject]
        public void Construct(IWebService webService)
        {
            _webService = webService;
        }
        
        public WeatherModel()
        {
            Data = new ReactiveProperty<WeatherData>();
        }

        public async void AsyncUpdate(CancellationTokenSource cancellationTokenSource)
        {
            Data.Value = await _webService.GetWeatherForecast(cancellationTokenSource);
            
            cancellationTokenSource.Cancel();
            
            Debug.Log("Weather data was updated.");
        }
    }
}