using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserealized<PlayerProgress>();

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.PlayerProgress.ToJson());
        }
    }
}