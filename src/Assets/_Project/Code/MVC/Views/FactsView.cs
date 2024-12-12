using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Project.Ui;
using Project.Web;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project
{
    public class FactsView : SimpleElement
    {
        [SerializeField] private Transform _rootBreedViews; 
        [SerializeField] private LoaderElement _loaderElement;
        [SerializeField] private InformationPopup _informationPopup;
   
        private BreedViewPool _breedViewPool;
        private List<BreedView> _breedViews = new List<BreedView>();
        private string _selectedBreedId = string.Empty;
        
        public event Action<string> OnRequestGetBreedData;
        
        [Inject]
        public void Construct(BreedViewPool breedViewPool)
        {
            _breedViewPool = breedViewPool;
        }
        
        public override void Show()
        {
            base.Show();
            
            _loaderElement.Show();
        }

        public override void Hide()
        {
            base.Hide();

            foreach (var view in _breedViews) 
                _breedViewPool.RemoveVied(view);
            
            _breedViews.Clear();
        }

        public void UpdateDisplay(List<BreedData> breeds)
        {
            _loaderElement.Hide();

            if (breeds == null)
                return;

            for (int i = 0; i < breeds.Count; i++)
            {
                var view = _breedViewPool.SpawnView(_rootBreedViews);

                view.UpdateDisplay(breeds[i], i + 1);

                _breedViews.Add(view);
            }

            foreach (var view in _breedViews) 
                view.OnClicked += OnClickedBreedHandler;
        }
        
        public void ShowBreedInformation(BreedData breed)
        {
            TryHideSelectedBreedViewPreloader();

            if (breed == null)
                return;

            _informationPopup.UpdateDisplay(breed);
            _informationPopup.Show();
        }

        private void OnClickedBreedHandler(string breedId)
        {
            TryHideSelectedBreedViewPreloader();
            
            var view = FindView(breedId);
            if (view != null) 
                view.SetActivePreloader(true);
            
            _selectedBreedId = breedId;
            OnRequestGetBreedData?.Invoke(breedId);
        }

        private BreedView FindView(string breedId)
        {
            var view = _breedViews.FirstOrDefault(x => x.BreedId == breedId);
            return view;
        }
        
        private void TryHideSelectedBreedViewPreloader()
        {
            if (_selectedBreedId != string.Empty)
            {
                var view = FindView(_selectedBreedId);
                if (view != null)
                    view.SetActivePreloader(false);
            }
        }
    }
}