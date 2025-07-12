using Assets.Scripts.Infrastructure.AssetManagment;
using Assets.Scripts.Infrastructure.Services.Ads;
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
        private readonly IAdsService _adsService;

        public UiFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService, IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService, _progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UiRootPath).transform;
    }
}