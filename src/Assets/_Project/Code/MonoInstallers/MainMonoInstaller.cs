using Project.Ui;
using Project.Web;
using UnityEngine;
using Zenject;

namespace Project.Installers
{
    public class MainMonoInstaller : MonoInstaller
    {
        [SerializeField] private ViewsProvider _viewsProvider;
        [SerializeField] private BreedView _breedViewPrefab;
        
        public override void InstallBindings()          
        {
            Container.BindInterfacesAndSelfTo<WebService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandInvoker>().AsSingle();
         
            Container.BindMemoryPool<BreedView, BreedView.Pool>().FromComponentInNewPrefab(_breedViewPrefab);
            Container.Bind<BreedViewPool>().AsSingle();
            
            Container.Bind<CommandFactory>().AsSingle();
            Container.BindFactory<WeatherModel, WeatherModelFactory>().AsSingle();
            Container.BindFactory<FactsModel, FactsModelFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<WeatherController>().AsSingle();
            Container.BindInterfacesAndSelfTo<FactsController>().AsSingle();
            
            Container.Bind<IViewsProvider>().FromInstance(_viewsProvider).AsSingle();
        }
    }
}
