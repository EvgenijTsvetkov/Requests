using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Ui
{
    public class ViewsProvider : MonoBehaviour, IViewsProvider
    {
        [SerializeField] private List<SimpleElement> _views = new List<SimpleElement>();

        public SimpleElement GetView<T>() where T: SimpleElement
        {
            return _views.OfType<T>().FirstOrDefault();
        }
    }
}