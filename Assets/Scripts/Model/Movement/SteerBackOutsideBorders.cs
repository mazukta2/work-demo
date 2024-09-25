using Infrastructure.Services;
using Model.Maps;
using Model.Physics;
using UnityEngine;
using Utilities;

namespace Model.Movement
{
    // return behaviour to position inside borders
    [RequireComponent(typeof(PhysicalBody))]
    public class SteerBackOutsideBorders : MonoBehaviour
    {
        [SerializeField] private float _angularSpeed = 1f;
        [SerializeField] private Map _map;
        
        private PhysicalBody _body;
        private Vector3? _targetPositionInsideBorders;

        protected void Awake()
        {
            _body = GetComponent<PhysicalBody>().ThrowExceptionIfNull();
            _angularSpeed.ThrowExceptionIfNegative();
            
            Service.FindIfNull(ref _map);
        }

        protected void Update()
        {
            ProcessOutOfBounds();
        }

        private void ProcessOutOfBounds()
        {
            var isOutside = _map.IsOutside(transform.position);

            if (isOutside)
            {
                if (_targetPositionInsideBorders == null)
                    _targetPositionInsideBorders = _map.GetRandomPosition();
                
                var planePosition = new Vector3(transform.position.x, 0, transform.position.z);
                var targetRotation = Quaternion.LookRotation(_targetPositionInsideBorders.Value - planePosition);
                
                _body.Rotate(targetRotation, _angularSpeed);
            }
        }

        #region Gizmos

        protected void OnDrawGizmos()
        {
            if (_targetPositionInsideBorders == null)
                return;
            
            var planePosition = new Vector3(transform.position.x, 0, transform.position.z);
            var targetRotation = Quaternion.LookRotation(_targetPositionInsideBorders.Value - planePosition);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + targetRotation * Vector3.forward * 2f);
        }

        #endregion
    }
}