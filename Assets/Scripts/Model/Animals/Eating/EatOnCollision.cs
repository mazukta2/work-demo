using Infrastructure.Services;
using UnityEngine;

namespace Model.Animals.Eating
{
    public class EatOnCollision : MonoBehaviour
    {
        [SerializeField] private Predator _predator;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _predator);
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
            _predator.Kill(otherGameObject);
        }
    }
}