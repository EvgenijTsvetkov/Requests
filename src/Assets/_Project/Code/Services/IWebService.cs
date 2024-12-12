using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Project.Web
{
    public interface IWebService
    {
        UniTask<WeatherData> GetWeatherForecast(CancellationTokenSource tokenSource);
        UniTask<List<BreedData>> GetBreeds(CancellationTokenSource tokenSource);
        UniTask<BreedData> GetBreedData(string breedId, CancellationTokenSource tokenSource);
    }
}