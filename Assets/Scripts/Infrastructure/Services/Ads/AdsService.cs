using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Ads
{
    public class AdsService : MonoBehaviour, IAdsService
    {
        public bool IsRewardedVideoReady { get; private set; }
        public int Reward { get;} = 10;
        public event Action RewardedVideoReady;

        private readonly string _androidAdUnitId = "Rewarded_Android";
        private readonly string _iosAdUnitId = "Rewarded_iOS";
        private readonly string _androidGameId = "5897621";
        private readonly string _IOSGameId = "5897620";
        private Action _onVideoFinished;

        private bool _isInitialized;

        IRewardedAd m_RewardedAd;

        public async void Initialize()
        {
            if (!_isInitialized)
            {
                try
                {
                    Debug.Log("Initializing...");
                    await UnityServices.InitializeAsync(GetGameId());
                    Debug.Log("Initialized!");
                    InitializationComplete();
                }
                catch (Exception e)
                {
                    InitializationFailed(e);
                }
            }
            else
            {
                Debug.LogWarning("AdService is initialized already!");
            }
        }

        public async void ShowRewarded(Action onVideoFinished)
        {
            if (m_RewardedAd?.AdState == AdState.Loaded)
            {
                try
                {
                    var showOptions = new RewardedAdShowOptions { AutoReload = true };
                    _onVideoFinished = onVideoFinished;
                    await m_RewardedAd.ShowAsync(showOptions);
                    Debug.Log("Rewarded Shown!");
                }
                catch (ShowFailedException e)
                {
                    Debug.LogWarning($"Rewarded failed to show: {e.Message}");
                }
            }
        }

        public async void ShowRewardedWithOptions(Action onVideoFinished)
        {
            if (m_RewardedAd?.AdState == AdState.Loaded)
            {
                try
                {
                    RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                    showOptions.AutoReload = true;
                    S2SRedeemData s2SData;
                    s2SData.UserId = "my cool user id";
                    s2SData.CustomData = "{\"reward\":\"Gems\",\"amount\":20}";
                    showOptions.S2SData = s2SData;

                    _onVideoFinished = onVideoFinished;
                    await m_RewardedAd.ShowAsync(showOptions); 
                    Debug.Log("Rewarded Shown!");
                    
                }
                catch (ShowFailedException e)
                {
                    Debug.LogWarning($"Rewarded failed to show: {e.Message}");
                }
            }
        }

        public void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
        }

        private void OnDestroy()
        {
            m_RewardedAd?.Dispose();
        }

        private InitializationOptions GetGameId()
        {
            var initializationOptions = new InitializationOptions();

#if UNITY_IOS
            if (!string.IsNullOrEmpty(_IOSGameId))
            {
                initializationOptions.SetGameId(_IOSGameId);
            }
#elif UNITY_ANDROID
            if (!string.IsNullOrEmpty(_androidGameId))
            {
                initializationOptions.SetGameId(_androidGameId);
            }
#endif

            return initializationOptions;
        }

        private async void LoadAd()
        {
            try
            {
                await m_RewardedAd.LoadAsync();
            }
            catch (LoadFailedException)
            {
                //OnFailedLoad callback
            }
        }

        private void InitializationComplete()
        {
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    m_RewardedAd = MediationService.Instance.CreateRewardedAd(_androidAdUnitId);
                    break;

                case RuntimePlatform.IPhonePlayer:
                    m_RewardedAd = MediationService.Instance.CreateRewardedAd(_iosAdUnitId);
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxEditor:
                    m_RewardedAd = MediationService.Instance.CreateRewardedAd(!string.IsNullOrEmpty(_androidAdUnitId)
                        ? _androidAdUnitId
                        : _iosAdUnitId);
                    break;
                default:
                    Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                    return;
            }

            m_RewardedAd.OnLoaded += AdLoaded;
            m_RewardedAd.OnFailedLoad += AdFailedLoad;

            m_RewardedAd.OnUserRewarded += UserRewarded;
            m_RewardedAd.OnClosed += AdClosed;

            Debug.Log($"AdService initialized. Loading Ad...");

            LoadAd();
            _isInitialized = true;
        }

        private void InitializationFailed(Exception error)
        {
            SdkInitializationError initializationError = SdkInitializationError.Unknown;
            if (error is InitializeFailedException initializeFailedException)
            {
                initializationError = initializeFailedException.initializationError;
            }

            Debug.Log($"Initialization Failed: {initializationError}:{error.Message}");
        }

        private void UserRewarded(object sender, RewardEventArgs e)
        {
            _onVideoFinished?.Invoke();
            Debug.Log($"User Rewarded! Type: {e.Type} Amount: {e.Amount}");
            _onVideoFinished = null;
        }

        private void AdClosed(object sender, EventArgs args)
        {
            IsRewardedVideoReady = false;
            Debug.Log("Rewarded Closed! Loading Ad...");
            if (m_RewardedAd?.AdState == AdState.Unloaded)
                LoadAd();
        }

        private void AdLoaded(object sender, EventArgs e)
        {
            IsRewardedVideoReady = true;
            RewardedVideoReady?.Invoke();
            Debug.Log("Ad loaded");
        }

        private void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            Debug.Log("Failed to load ad");
            Debug.Log(e.Message);
        }
    }
}
