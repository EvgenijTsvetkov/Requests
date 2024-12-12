using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Ui
{
    public class MainPanel : MonoBehaviour
    {
        [Header("Toggles")]
        [SerializeField] private Toggle _weatherToggle;
        [SerializeField] private Toggle _factsToggle;
       
        private WeatherController _weatherController;
        private FactsController _factsController;
        
        [Inject]
        public void Construct(WeatherController weatherController, FactsController factsController)
        {
            _weatherController = weatherController;
            _factsController = factsController;
        }
        
        private void Awake()
        {
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }

        private void Start()
        {
            _weatherController.ActivateLoopUpdate();
        }
        
        private void OnWeatherToggleChangedHandler(bool value)
        {
            if (value)
            {
                _weatherController.ActivateLoopUpdate();
            }
            
            else
                _weatherController.DeactivateLoopUpdate();

            _weatherToggle.interactable = !value;
        }
        
        private void OnFactsToggleChangedHandler(bool value)
        {
            if (value)
            {
                _factsController.ShowView();
                _factsController.CreateUpdateFactsCommand();
            }
            
            else
                _factsController.HideView();
            
            _factsToggle.interactable = !value;
        }

        private void SubscribeOnEvents()
        {
            if (_weatherToggle != null) 
                _weatherToggle.onValueChanged.AddListener(OnWeatherToggleChangedHandler);
            
            if (_factsToggle != null) 
                _factsToggle.onValueChanged.AddListener(OnFactsToggleChangedHandler);
        }

        private void UnsubscribeOnEvents()
        {
            if (_weatherToggle != null) 
                _weatherToggle.onValueChanged.RemoveListener(OnWeatherToggleChangedHandler);
            
            if (_factsToggle != null) 
                _factsToggle.onValueChanged.RemoveListener(OnFactsToggleChangedHandler);
        }
    }
}