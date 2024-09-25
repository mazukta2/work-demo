using Model.Physics;
using UnityEngine;
using Utilities;

namespace Model.Movement
{
    [RequireComponent(typeof(PhysicalBody))]
    public class ForwardMovement : MonoBehaviour
    {
        [SerializeField] private float _movingSpeed = 1f;
        
        private PhysicalBody _body;

        protected void Awake()
        {
            _body = GetComponent<PhysicalBody>().ThrowExceptionIfNull();
            _movingSpeed.ThrowExceptionIfNegative();
        }
        
        protected void Update()
        {
            if (!_body.IsGrounded)
                return;
         
            //if (Time.time <= _lastCollision + 1f)
            //    return;
            
            _body.Move(_body.Forward, _movingSpeed * Time.deltaTime);
        }

    }
}