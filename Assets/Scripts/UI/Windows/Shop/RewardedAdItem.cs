using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;
        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        public void Initialize()
        {
            ShowAdButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Subscribe() => 
            _adsService.RewardedVideoReady += RefreshAvailableAd;

        public void Cleanup() => 
            _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() => 
            _adsService.ShowRewarded(OnVideoFinished);

        private void OnVideoFinished() => 
            _progressService.PlayerProgress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adActiveObject in AdActiveObjects)
                adActiveObject.SetActive(videoReady);

            foreach (GameObject adInactiveObject in AdInactiveObjects)
                adInactiveObject.SetActive(!videoReady);
        }
    }
}
