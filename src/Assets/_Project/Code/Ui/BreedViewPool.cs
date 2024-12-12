using UnityEngine;

namespace Project.Ui
{
    public class BreedViewPool
    {
        private BreedView.Pool _pool;

        public BreedViewPool(BreedView.Pool pool)
        {
            _pool = pool;
        }
        
        public BreedView SpawnView(Transform root)
        {
            var item = _pool.Spawn(root);

            item.transform.SetParent(root);
            item.transform.SetAsLastSibling();
            
            return item;
        }

        public void RemoveVied(BreedView view)
        {
            _pool.Despawn(view);
        }
    }
}