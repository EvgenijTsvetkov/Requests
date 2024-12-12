using Zenject;

namespace Project
{
    public class WeatherModelFactory : PlaceholderFactory<WeatherModel>
    {
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public override WeatherModel Create()
        {
            return _instantiator.Instantiate<WeatherModel>();
        }
    }
}