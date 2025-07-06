using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        public float PlayerSpeed = 8;

        [HideInInspector] public Vector2 MoveDirection;

        [SerializeField] private PlayerAnimator _playerAnimator;

        private IInputService _inputService;


        void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        void Update()
        {
            MoveDirection = _inputService.Axis;
            MovePlayer(_inputService.Axis);
            RotatePlayer(_inputService.Axis);
        }

        public void MovePlayer(Vector2 moveDirection)
        {
            transform.position += PlayerSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0, moveDirection.y).normalized;
        }

        public void RotatePlayer(Vector2 rotation)
        {
            if (rotation != Vector2.zero)
                transform.rotation = Quaternion.LookRotation(new Vector3(rotation.x, 0, rotation.y));
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                    Warp(savedPosition);
            }
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        private void Warp(Vector3Data to) =>
            transform.position = to.AsUnityVector().WithY(transform.localScale.y * 2);
    }
}
