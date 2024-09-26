using UnityEngine;
using Utilities;

namespace Model.GameObjects
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float _time = 1f;
        
        private void Awake()
        {
            _time.ThrowExceptionIfNegative();
            Destroy(gameObject, _time);
        }
    }
}