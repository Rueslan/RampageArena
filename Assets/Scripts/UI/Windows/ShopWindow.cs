using TMPro;

namespace Assets.Scripts.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        public TextMeshProUGUI CoinText;

        protected override void Initialize() =>
            RefreshCoinText();

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            Progress.WorldData.LootData.Changed += RefreshCoinText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshCoinText;
        }

        private void RefreshCoinText() => 
            CoinText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}