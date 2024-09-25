using System;
using UnityEngine;
using Utilities;

namespace Model.Behaviours
{
    public class IsKilled : MonoBehaviour
    {
        [SerializeField] private GameObject _killer;

        protected void Start()
        {
            _killer.ThrowExceptionIfNull();
        }

        protected void LateUpdate()
        {
            Destroy(gameObject);
        }

        public void SetKiller(GameObject killer)
        {
            _killer = killer;
        }
    }
}