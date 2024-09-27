using UnityEngine;
using Utilities;

namespace Model.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicalBody : MonoBehaviour
    {
        [SerializeField] private float _rotationOnCollision = 1;
        [SerializeField] private ForceMode _jumpMode;
        [SerializeField] private string _groundTag = "Ground";
        private Rigidbody _rigidbody;
        private bool _isGrounded;

        public bool IsGrounded => _isGrounded;
        public Vector3 Forward => transform.forward;

        protected void Awake()  
        {
            _rigidbody = GetComponent<Rigidbody>().ThrowExceptionIfNull();
            _rotationOnCollision.ThrowExceptionIfNegative();
        }
        
        protected void OnCollisionEnter(Collision collision)
        {
            // check grounding
            if (collision.collider.CompareTag(_groundTag))
                _isGrounded = true;
        }
        
        protected void OnCollisionExit(Collision collision)
        {
            // check grounding
            if (collision.collider.CompareTag(_groundTag))
                _isGrounded = false;
        }

        public void JumpOf(Vector3 middlePoint, float collisionJumpForce, bool withRandomTorque = true)
        {
            _rigidbody.AddExplosionForce(collisionJumpForce, middlePoint, 
                10, collisionJumpForce, _jumpMode);
            
            if (withRandomTorque)
                _rigidbody.AddTorque(0,Random.Range(-_rotationOnCollision, _rotationOnCollision),0, _jumpMode);
        }

        public void Jump(Vector3 bodyForward, float jumpForce)
        {
            _rigidbody.AddForce((bodyForward + Vector3.up) * jumpForce, _jumpMode);
        }

        public void Move(Vector3 direction, float movingSpeed)
        {
            _rigidbody.MovePosition(transform.position + direction * movingSpeed);
        }

        public void Rotate(Quaternion targetRotation, float angularSpeed)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation,
                targetRotation, angularSpeed));
        }
    }
}