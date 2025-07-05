using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.StaticData;
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

            RegisterServicies();
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


        private void RegisterServicies()
        {
            RegisterInputService();
            IStaticDataService staticDataService = RegisterStaticData();
            IAssets assets = RegisterAssetsProvider();
            IRandomService randomService = RegisterRandomService();
            IPersistentProgressService progressService = RegisterProgressService();
            _services.RegisterSingle<IGameFactory>(new GameFactory(assets, staticDataService, randomService, progressService));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
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
            IAssets assets = new AssetProvider();
            _services.RegisterSingle(assets);
            return assets;
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

