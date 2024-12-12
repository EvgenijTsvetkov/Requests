using Project.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class WeatherView : SimpleElement
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LoaderElement _loaderElement;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _temperature;

        private void Start()
        {
            Reset();
        }

        public override void Show()
        {
            base.Show();

            if (_icon.sprite == null) 
                _loaderElement.Show();
        }

        public void UpdateDisplay(WeatherData data)
        {
            _icon.sprite = data.Icon;
            _temperature.text = $"Today {data.Temperature}";
        
            _canvasGroup.alpha = 1;
            _loaderElement.Hide();
        }

        private void Reset()
        {
            _canvasGroup.alpha = 0;
        }
    }
}