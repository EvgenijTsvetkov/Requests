using System.Collections.Generic;
using System.Threading;
using Project.Web;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class FactsModel
    {
        private IWebService _webService;
        
        public ReactiveProperty<List<BreedData>> Breeds { get; }
        public ReactiveProperty<BreedData> SelectedBreed { get; }

        [Inject]
        public void Construct(IWebService webService)
        {
            _webService = webService;
        }
        
        public FactsModel()
        {
            Breeds = new ReactiveProperty<List<BreedData>>();
            SelectedBreed = new ReactiveProperty<BreedData>();
        }
        
        public async void UpdateBreeds(CancellationTokenSource tokenSource)
        {
            Breeds.Value = await _webService.GetBreeds(tokenSource);
            
            tokenSource.Cancel();
            
            Debug.Log("Breeds was updated.");
        }

        public async void GetBreedData(string breedId, CancellationTokenSource tokenSource)
        { 
            SelectedBreed.Value = await _webService.GetBreedData(breedId, tokenSource);
            
            tokenSource.Cancel();
        }
    }
}