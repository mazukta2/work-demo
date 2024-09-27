using System;
using Model.Animals;
using Model.Behaviours;
using UnityEngine;

namespace Model.Killing
{
    public class KillingManager : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnKill = delegate { };
        public int PreyKillCount { get; private set; }
        public int PredatorKillCount { get; private set; }

        public void Kill(GameObject killer, GameObject victim)
        {
            if (victim.GetComponent<IsKilled>() != null)
                return;
            
            var isKilled = victim.AddComponent<IsKilled>();
            isKilled.SetKiller(killer);

            if (victim.GetComponent<IsPrey>())
                PreyKillCount++;

            if (victim.GetComponent<IsPredator>())
                PredatorKillCount++;
            
            OnKill(killer, victim);
        }
    }
}