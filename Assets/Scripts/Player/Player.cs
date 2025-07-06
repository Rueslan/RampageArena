using Assets.Scripts.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : PlayerBase
    {
        private void Start()
        {
            PlayersList.AddPlayer(gameObject);
        }

        public void Restore()
        {
            EventManager.CallPlayerRestore();
            transform.position = new Vector3(0, 1, 0);
            PlayersList.AddPlayer(gameObject);
        }
    }
}
