using System.Collections;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class NavMeshSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _delay = 5f;
        [SerializeField] private int _limit = 10;
        [SerializeField] private int _scaleMultiplier = 2;
        [SerializeField] private float _Yoffset = 0;
        private int _currentAmount = 0;

        private float _maxDistance = 10f;

        void Start()
        {
            StartCoroutine(Spawner());
        }

        public IEnumerator Spawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);

                if (_currentAmount < _limit)
                {
                    Instantiate(_prefab, GetNavMeshRandomPoint() + Vector3.up * _Yoffset, Quaternion.identity).With(x => x.transform.localScale = Vector3.one * _scaleMultiplier);
                    _currentAmount++;
                }

                else break;
            }
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(-60f, 15f), 0f, Random.Range(-40f, 40f));
        }

        private Vector3 GetNavMeshRandomPoint()
        {
            Vector3 randomPoint = GetRandomPoint();

            NavMeshHit hit;
            while (!NavMesh.SamplePosition(randomPoint, out hit, _maxDistance, NavMesh.AllAreas))
            {
                randomPoint = GetRandomPoint();
            }

            return randomPoint;
        }
    }
}
