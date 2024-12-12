using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Ui
{
    public class LoaderElement : SimpleElement
    {
        [SerializeField] private Image _loadingCircle;
        [SerializeField] private float _rotationTime = 10f;

        private Sequence _sequence;

        private void Awake()
        {
            CreateSequence();
        }

        private void OnEnable()
        {
            _sequence.Play();
        }

        private void OnDisable()
        {
            _sequence.Pause();
        }

        private void CreateSequence()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(_loadingCircle.rectTransform.DOLocalRotate(new Vector3(0, 0, 360f), _rotationTime,
                    RotateMode.FastBeyond360))
                .SetLoops(-1);
        }
    }
}