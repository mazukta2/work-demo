using Infrastructure.GameEvents;
using Infrastructure.Services;
using UnityEngine;

namespace Model.Animals.Eating
{
    public class SpawnOnKill : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameEvent<Predator.OnPredatorKilledEvent> _onKill;

        protected void OnEnable()
        {
            _onKill.OnEvent += HandleOnKill;
        }

        protected void OnDisable()
        {
            _onKill.OnEvent -= HandleOnKill;
        }

        private void HandleOnKill(Predator.OnPredatorKilledEvent message)
        {
            Instantiate(_prefab, message.Killer.transform.position, Quaternion.identity, transform);
        }
    }
}