using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Input;
using Assets.Scripts.StaticData;
using Assets.Scripts.UI.Services.Factory;
using Assets.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INIT = "Init";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {            
            _sceneLoader.Load(INIT, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();


        private void RegisterServices()
        {
            RegisterInputService();
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            IAdsService adsService = RegisterAdsService();
            IStaticDataService staticDataService = RegisterStaticData();
            IAssets assets = RegisterAssetsProvider();
            IRandomService randomService = RegisterRandomService();
            IPersistentProgressService progressService = RegisterProgressService();
            IUIFactory uiFactory = RegisterUIFactory(assets, staticDataService, progressService, adsService);
            IWindowService windowService = RegisterWindowService(uiFactory);
            _services.RegisterSingle<IGameFactory>(new GameFactory(assets, staticDataService, randomService, progressService, windowService, _stateMachine));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService, _services.Single<IGameFactory>()));
        }

        private IAdsService RegisterAdsService()
        {
            var adsService = new AdsService();
            _services.RegisterSingle<IAdsService>(adsService);
            adsService.Initialize();
            return adsService;
        }

        private IWindowService RegisterWindowService(IUIFactory uiFactory)
        {
            IWindowService windowService = new WindowService(uiFactory);
            return windowService;
        }

        private IUIFactory RegisterUIFactory(IAssets assets, IStaticDataService staticDataService,
            IPersistentProgressService progressService, IAdsService adsService)
        {
            IUIFactory uiFactory = new UiFactory(assets, staticDataService, progressService, adsService);
            _services.RegisterSingle(uiFactory);
            return uiFactory;
        }

        private IPersistentProgressService RegisterProgressService()
        {
            IPersistentProgressService progressService = new PersistentProgressService();
            _services.RegisterSingle(progressService);
            return progressService;
        }

        private IRandomService RegisterRandomService()
        {
            IRandomService randomService = new RandomService();
            _services.RegisterSingle(randomService);
            return randomService;
        }

        private void RegisterInputService()
        {
            _services.RegisterSingle(InputService());
        }

        private IAssets RegisterAssetsProvider()
        {
            IAssets assetProvider = new AssetProvider();
            assetProvider.Initialize();
            _services.RegisterSingle(assetProvider);
            return assetProvider;
        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
            return staticData;
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}

