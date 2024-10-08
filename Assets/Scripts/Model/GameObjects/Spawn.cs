﻿using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Model.GameObjects
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private float _minTime = 1;
        [SerializeField] private float _maxTime = 2;
        [SerializeField] private int _maximumChildren = 10;
        [SerializeField] Bounds _bounds;

        private float _spawnTime;

        public void Awake()
        {
            AssertUtilities.ThrowExceptionIfNotTrue(_prefabs.Length > 0, "Prefabs list is empty");
            _minTime.ThrowExceptionIfNegative().ThrowExceptionIfMoreThan(_maxTime);
        }

        protected void Update()
        {
            if (transform.childCount >= _maximumChildren)
                return;
            
            if (Time.time >= _spawnTime)
            {
                _spawnTime = Time.time + Random.Range(_minTime, _maxTime);
                var position = new Vector3
                (
                    Random.Range(_bounds.min.x, _bounds.max.x),
                    Random.Range(_bounds.min.y, _bounds.max.y),
                    Random.Range(_bounds.min.z, _bounds.max.z)
                );
                var rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
                
                var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
                Instantiate(prefab, position, rotation, transform);
            }
        }

        #region Gizmos

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        }

        #endregion
    }
}