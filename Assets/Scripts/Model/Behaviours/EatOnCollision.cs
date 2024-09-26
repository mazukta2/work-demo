using System;
using Infrastructure.Services;
using Model.Animals;
using Model.Killing;
using UnityEngine;

namespace Model.Behaviours
{
    public class EatOnCollision : MonoBehaviour
    {
        [SerializeField] private KillingManager _killingManager;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _killingManager);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (GetComponent<IsKilled>())
                return;
            
            var otherRigidbody = collision.rigidbody;
            if (otherRigidbody != null)
            {
                var isPrey = otherRigidbody.GetComponent<IsPrey>() != null;
                if (isPrey)
                {
                    Eat(otherRigidbody.gameObject);
                    return;
                }
                
                var isPredator = otherRigidbody.GetComponent<IsPredator>() != null;
                if (isPredator)
                {
                    Eat(otherRigidbody.gameObject);
                    return;
                }
            }
        }

        private void Eat(GameObject otherGameObject)
        {
            _killingManager.Kill(gameObject, otherGameObject);
        }
    }
}