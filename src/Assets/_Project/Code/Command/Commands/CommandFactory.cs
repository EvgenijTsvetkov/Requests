using System;
using Zenject;

namespace Project
{
    public class CommandFactory : IFactory<CommandType, ICommand>
    {
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public ICommand Create(CommandType type)
        {
            switch (type)
            {
                case CommandType.UpdateWeather:
                    return _instantiator.Instantiate<GetWeatherForecast>();
                
                case CommandType.UpdateFacts:
                    return _instantiator.Instantiate<GetBreeds>();
                
                case CommandType.GetBreedData:
                    return _instantiator.Instantiate<GetBreedData>();
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}