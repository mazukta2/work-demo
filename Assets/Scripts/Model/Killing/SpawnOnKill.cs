﻿using System;
using Infrastructure.Services;
using UnityEngine;

namespace Model.Killing
{
    public class SpawnOnKill : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private KillingManager _killingManager;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _killingManager);
            _killingManager.OnKill += HandleOnKill;
        }

        protected void OnDisable()
        {
            _killingManager.OnKill -= HandleOnKill;
        }

        private void HandleOnKill(GameObject killer, GameObject victim)
        {
            Instantiate(_prefab, killer.transform.position, Quaternion.identity, transform);
        }
    }
}