using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Services;
using Assets.Scripts.StaticData.Windows;
using Assets.Scripts.UI.Services.Windows;
using Assets.Scripts.UI.Windows;
using UnityEngine;

namespace Assets.Scripts.UI.Services.Factory
{
    public class UiFactory : IUIFactory
    {
        const string UiRootPath = "UI/UIRoot";
        private readonly IAssets _assets;
        private Transform  _uiRoot;
        private readonly IStaticDataService  _staticData;
        private readonly IPersistentProgressService _progressService;

        public UiFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UiRootPath).transform;
    }
}