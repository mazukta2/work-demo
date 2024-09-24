using System;
using Infrastructure.Services;
using Model.Maps;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Model.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RandomMovement : MonoBehaviour
    {
        [SerializeField] private float _movingSpeed = 1f;
        [SerializeField] private float _angularSpeed = 1f;
        [SerializeField] private float _minPause = 1f;
        [SerializeField] private float _maxPause = 5f;
        [SerializeField] private Map _map;
        
        private const float RotationThreshold = 0.1f;
        private Rigidbody _rigidbody;
        private Quaternion _targetRotation;
        private bool _isOutside;

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>().ThrowExceptionIfNull();
            _movingSpeed.ThrowExceptionIfNegative();
            _angularSpeed.ThrowExceptionIfNegative();
            _minPause.ThrowExceptionIfNegative().ThrowExceptionIfMoreThan(_maxPause);

            Service.FindIfNull(ref _map);
        }

        protected void OnEnable()
        {
            // randomize starting rotation
            _targetRotation = transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        protected void Update()
        {
            ProcessOutOfBounds();
            ProcessMovement();
        }

        #region Processing

        private void ProcessOutOfBounds()
        {
            var isOutside = _map.IsOutside(transform.position);
            
            if (!_isOutside && isOutside)
            {
                var target = _map.GetRandomPosition();
                _targetRotation = Quaternion.LookRotation(target - transform.position);
                _isOutside = true;
            }
            else if (_isOutside && !isOutside)
                _isOutside = false;
        }

        private void ProcessMovement()
        {
            if (!IsRotationNeeded())
            {
                // move forward
                _rigidbody.velocity = transform.forward * _movingSpeed;
            }
            else
            {
                // stop to rotate
                _rigidbody.velocity = Vector3.zero;
                // rotate to target rotation
                _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation,
                    _targetRotation, _angularSpeed * Time.deltaTime));
            }
        }

        #endregion
        
        #region Support

        private bool IsRotationNeeded()
        {
            return Quaternion.Angle(_targetRotation, transform.rotation) > RotationThreshold;
        }

        #endregion

        #region Gizmos

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, _targetRotation * Vector3.forward * 2f);
        }

        #endregion
    }
}