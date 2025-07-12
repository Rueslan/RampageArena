using System;
using Unity.Services.Mediation;

namespace Assets.Scripts.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        void ShowRewarded(Action onVideoFinished);
        void ShowRewardedWithOptions(Action onVideoFinished);
        void Initialize();
        void ImpressionEvent(object sender, ImpressionEventArgs args);
        bool IsRewardedVideoReady { get; }
        int Reward { get; }
        event Action RewardedVideoReady;
    }
}