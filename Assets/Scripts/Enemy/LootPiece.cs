using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Logic;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class LootPiece: MonoBehaviour, ISavedProgress
    {
        public GameObject Coin;
        public GameObject PickupFxPrefab;
        public TextMeshProUGUI LootText;
        public GameObject PickupPopup;
        public Vector3Data Position;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;
        private float _destroyDelay = 1.5f;
        private string _uniqueId;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _uniqueId = GetComponent<UniqueId>().Id;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
            Position = transform.position.AsVectorData();
        }

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideCoin();
            PlayPickupFx();
            ShowText();
            StartCoroutine(DestroyTimer());
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private void HideCoin() => 
            Coin.SetActive(false);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }

        private void PlayPickupFx() => 
            Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (!_picked)
            {
                _worldData.LootData.LootPositions.Add(_uniqueId, Position);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (_worldData.LootData.LootPositions.ContainsKey(_uniqueId))
            {
                _worldData.LootData.LootPositions.TryGetValue(_uniqueId, out Position);
                _worldData.LootData.LootPositions.Remove(_uniqueId);
                transform.position = Position.AsUnityVector();
            }
        }
    }
}
