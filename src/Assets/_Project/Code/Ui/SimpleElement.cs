using UnityEngine;

namespace Project.Ui
{
    public class SimpleElement : MonoBehaviour
    {
        [SerializeField] protected GameObject _content;

        public virtual void Show()
        {
            if (_content != null)
                _content.SetActive(true);
        }

        public virtual void Hide()
        {
            if (_content != null)
                _content.SetActive(false);
        }
    }
}