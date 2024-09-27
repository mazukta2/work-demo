using System;
using Infrastructure.Services;
using Model.Killing;
using TMPro;
using UnityEngine;

namespace Model.UI
{
    public class KillMeter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _preyValue;
        [SerializeField] private TMP_Text _predatorValue;
        [SerializeField] private KillingManager _killingManager;

        protected void OnEnable()
        {
            Service.FindIfNull(ref _killingManager);
            _killingManager.OnKill += HandleOnKill;
            UpdateView();
        }

        protected void OnDisable()
        {
            _killingManager.OnKill -= HandleOnKill;
        }

        private void HandleOnKill(GameObject killer, GameObject victim)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _preyValue.text = _killingManager.PreyKillCount.ToString();
            _predatorValue.text = _killingManager.PredatorKillCount.ToString();   
        }
    }
}