using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Logic;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private const string ENEMY_SPAWNER_TAG = "EnemySpawner";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders() => 
            _gameFactory.ProgressReaders.ForEach(x => x.LoadProgress(_progressService.PlayerProgress));


        private void InitGameWorld()
        {
            InitSpawners();

            GameObject player = InitPlayer();
            InitHUD(player);
            CameraFollow(player);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(ENEMY_SPAWNER_TAG))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private GameObject InitPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(INITIAL_POINT_TAG));

        private void InitHUD(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<IHealth>());
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponent<FollowingCamera>().Follow(player);
        }

        public void Enter()
        {

        }
    }
}