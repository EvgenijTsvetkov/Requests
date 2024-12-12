using System;
using Cysharp.Threading.Tasks;
using Project.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Ui
{
    public class BreedView : SimpleElement
    {
        [SerializeField] private Button _button;
        [SerializeField] private SimpleElement _preloader;

        [SerializeField] private TMP_Text _index;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        private string _breedId;
        
        public string BreedId => _breedId;
        
        public event Action<string> OnClicked;
    
        private void OnEnable()
        {
            SubscribeOnEvents();
        }
        
        private void OnDisable()
        {
            UnsubscribeOnEvents();
        }

        public void UpdateDisplay(BreedData data, int index)
        {
            _breedId = data.id;

            _index.text = $"{index}.";
            _name.text = data.attributes.name;
            _description.text = data.attributes.description;
        }

        private void OnClickHandler()
        {
            OnClicked?.Invoke(_breedId);
        }

        public void SetActivePreloader(bool value)
        {
            if (value)
                _preloader.Show();
            
            else
                _preloader.Hide();
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(OnClickHandler);
        }

        private void UnsubscribeOnEvents()
        {
            _button.onClick.RemoveListener(OnClickHandler);
        }
        
        public class Pool : MemoryPool<Transform, BreedView> 
        {
            protected override void OnSpawned(BreedView item)
            {
                base.OnSpawned(item);
                
                item.Show();
            }

            protected override void OnDespawned(BreedView item)
            {
                base.OnDespawned(item);
                
                item.Hide();
            }
        }
    }
}