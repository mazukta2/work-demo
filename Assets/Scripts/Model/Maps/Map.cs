using System;
using UnityEngine;
using Utilities;

namespace Model.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Bounds _bounds;

        protected void Awake()
        {
            AssertUtilities.Assert(_bounds.size != Vector3.zero, "Map size can't be zero");
        }

        public bool IsOutside(Vector3 position)
        {
            return !_bounds.Contains(position);
        }

        public Vector3 GetRandomPosition()
        {
            return _bounds.center + 
                   new Vector3(UnityEngine.Random.Range(-_bounds.extents.x, _bounds.extents.x), 
                       0, UnityEngine.Random.Range(-_bounds.extents.z, _bounds.extents.z));
        }

        #region Gizmos

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        }

        #endregion
        
    }
}