using Model.Animals;
using Model.Physics;
using UnityEngine;
using Utilities;

namespace Model.Movement
{
    public class JumpOfOnCollisionWithPray : MonoBehaviour
    {
        [SerializeField] private float _collisionJumpForce = 1f;
        
        private PhysicalBody _body;

        protected void Awake()
        {
            _body = GetComponent<PhysicalBody>().ThrowExceptionIfNull();
            _collisionJumpForce.ThrowExceptionIfNegative();
        }

        protected void OnCollisionEnter(Collision collision)
        {
            var otherRigidbody = collision.rigidbody;
            if (otherRigidbody != null)
            {
                var isPrey = otherRigidbody.GetComponent<IsPrey>() != null;
                if (isPrey)
                {
                    //_lastCollision = Time.time;
                    var middlePoint = Vector3.Lerp(otherRigidbody.transform.position, transform.position, 0.5f);
                    _body.JumpOf(middlePoint, _collisionJumpForce);
                }
            }
        }

    }
}