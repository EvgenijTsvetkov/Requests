﻿using Zenject;

namespace Project
{
    public class FactsrModelFactory : PlaceholderFactory<FactsModel>
    {
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public override FactsModel Create()
        {
            return _instantiator.Instantiate<FactsModel>();
        }
    }
}