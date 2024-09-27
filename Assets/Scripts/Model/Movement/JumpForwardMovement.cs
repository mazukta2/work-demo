using Model.Physics;
using UnityEngine;
using Utilities;

namespace Model.Movement
{
    [RequireComponent(typeof(PhysicalBody))]
    public class JumpForwardMovement : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 1f;
        
        private PhysicalBody _body;

        protected void Awake()
        {
            _body = GetComponent<PhysicalBody>().ThrowExceptionIfNull();
            _jumpForce.ThrowExceptionIfNegative();
        }
        
        protected void Update()
        {
            if (!_body.IsGrounded)
                return;
         
            //if (Time.time <= _lastCollision + 1f)
            //    return;
            
            _body.Jump(_body.Forward, _jumpForce);
        }

    }
}