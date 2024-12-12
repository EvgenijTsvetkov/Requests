using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Ui
{
    public class InformationPopup : SimpleElement
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _closeutButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        private const float TweenFadeinDuration = 0.6f;
        private const float TweenFadeOutDuration = 0.3f;
        
        private void OnEnable()
        {
            SubscribeOnEvents();
        }

        private void OnDisable()
        {
            UnsubscribeOnEvents();
        }

        public override void Show()
        {
            base.Show();
            
            _canvasGroup.DOFade(1f, TweenFadeinDuration);
        }

        public override void Hide()
        {
            _canvasGroup.DOFade(0f, TweenFadeOutDuration)
                .OnComplete(() => base.Hide());
        }

        public void UpdateDisplay(BreedData breed)
        {
            _title.text = breed.attributes.name;
            _description.text = breed.attributes.description;
        }
        
        private void SubscribeOnEvents()
        {
            _closeutButton.onClick.AddListener(Hide);
        }

        private void UnsubscribeOnEvents()
        {
            _closeutButton.onClick.RemoveListener(Hide);
        }
    }
}