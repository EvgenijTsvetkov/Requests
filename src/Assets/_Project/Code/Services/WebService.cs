using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Web
{
    public class WebService : IWebService
    {
        public async UniTask<WeatherData> GetWeatherForecast(CancellationTokenSource tokenSource)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get("https://api.weather.gov/gridpoints/TOP/32,81/forecast")
                .SendWebRequest()
                .WithCancellation(tokenSource.Token);

            WeatherRoot root = JsonConvert.DeserializeObject<WeatherRoot>(request.downloadHandler.text);
            Period period = root.properties.periods[0];
            
            request = await UnityWebRequestTexture
                .GetTexture(period.icon)
                .SendWebRequest()
                .WithCancellation(tokenSource.Token);

            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            return new WeatherData
                {Temperature = $"{period.temperature} {period.temperatureUnit}", Icon = sprite};
        }

        public async UniTask<List<BreedData>> GetBreeds(CancellationTokenSource tokenSource)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get("https://dogapi.dog/api/v2/breeds")
                .SendWebRequest()
                .WithCancellation(tokenSource.Token);
            
            
            BreedsRoot root = JsonConvert.DeserializeObject<BreedsRoot>(request.downloadHandler.text);
           
            return root.data;
        }

        public async UniTask<BreedData> GetBreedData(string breedId, CancellationTokenSource tokenSource)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get($"https://dogapi.dog/api/v2/breeds/{breedId}")
                .SendWebRequest()
                .WithCancellation(tokenSource.Token);
            
            BreedRoot root = JsonConvert.DeserializeObject<BreedRoot>(request.downloadHandler.text);
            
            return root.data;
        }
    }
    
    public class Period
    {
        public string name { get; set; }
        public int temperature { get; set; }
        public string temperatureUnit { get; set; }
        public string icon { get; set; }
    }

    public class Properties
    {
        public List<Period> periods { get; set; }
    }

    public class WeatherRoot
    {
        public Properties properties { get; set; }
    }
    
    public class Attributes
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class BreedData
    {
        public string id { get; set; }
        public Attributes attributes { get; set; }
    }

    public class BreedsRoot
    {
        public List<BreedData> data { get; set; }
    }

    public class BreedRoot
    {
        public BreedData data { get; set; }
    }
}