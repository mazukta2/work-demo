using System;
using Model.Animals;
using Model.Behaviours;
using UnityEngine;

namespace Model.Killing
{
    // TODO: Prey and predator counts are hardcoded. Consider using ScriptableObject tags, to set it in editor.
    public class KillingManager : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnKill = delegate { };
        public int PreyKillCount { get; private set; }
        public int PredatorKillCount { get; private set; }

        public void Kill(GameObject killer, GameObject victim)
        {
            if (victim.GetComponent<IsDead>() != null)
                return;
            
            var isKilled = victim.AddComponent<IsDead>();

            if (victim.GetComponent<IsPrey>())
                PreyKillCount++;

            if (victim.GetComponent<IsPredator>())
                PredatorKillCount++;
            
            OnKill(killer, victim);
        }
    }
}