using Model.Animals;
using UnityEngine;

namespace Model.Behaviours
{
    public class EatOnCollision : MonoBehaviour
    {
        
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
            if (otherGameObject.GetComponent<IsKilled>() == null)
            {
                var killed = otherGameObject.AddComponent<IsKilled>();
                killed.SetKiller(gameObject);
            }
        }
    }
}