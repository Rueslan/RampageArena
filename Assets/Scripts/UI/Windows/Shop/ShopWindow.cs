using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.UI.Windows.Shop;
using TMPro;

namespace Assets.Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI CoinText;
        public RewardedAdItem AdItem;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            base.Construct(progressService);
            AdItem.Construct(adsService, progressService);
        }

        protected override void Initialize()
        {
            AdItem.Initialize();
            RefreshCoinText();
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            AdItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshCoinText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            AdItem.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshCoinText;
        }

        private void RefreshCoinText() => 
            CoinText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}