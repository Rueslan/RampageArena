using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using Zenject;

public class ThirdPersonPlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private TMP_Text _scoreText;

    public override void InstallBindings()
    {
        //BindPlayer();
    }

    //private void BindPlayer()
    //{
    //    var playerInstance =
    //                Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

    //    playerInstance.GetComponent<ScoreHandler>().Construct(_scoreText);

    //    Container.Bind<Player>().
    //        FromInstance(playerInstance).
    //        AsSingle().
    //        NonLazy();

    //    Container.
    //        Bind<PlayerAttackHandler>().
    //        FromComponentOn(playerInstance.gameObject).
    //        AsSingle();
    //}
}