using Infrastructure.Services;
using UnityEngine;

namespace Model.Animals.Eating
{
    public class SpawnOnKill : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [Header("Services")]
        [SerializeField] private KillingManager _killingManager;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _killingManager);
            _killingManager.OnKill += HandleOnKill;
        }

        protected void OnDisable()
        {
            _killingManager.OnKill -= HandleOnKill;
        }

        private void HandleOnKill(GameObject killer, GameObject victim)
        {
            Instantiate(_prefab, killer.transform.position, Quaternion.identity, transform);
        }
    }
}