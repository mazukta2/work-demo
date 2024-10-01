using Infrastructure.Services;
using UnityEngine;

namespace Model.Animals.Eating
{
    public class EatOnCollision : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private KillingManager _killingManager;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _killingManager);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (GetComponent<IsDead>())
                return;
            
            var otherRigidbody = collision.rigidbody;
            if (otherRigidbody != null)
            {
                Eat(otherRigidbody.gameObject);
            }
        }

        private void Eat(GameObject otherGameObject)
        {
            _killingManager.Kill(gameObject, otherGameObject);
        }
    }
}