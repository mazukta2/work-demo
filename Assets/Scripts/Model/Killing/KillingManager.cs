using System;
using Model.Behaviours;
using UnityEngine;

namespace Model.Killing
{
    public class KillingManager : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnKill = delegate { };
        
        public void Kill(GameObject killer, GameObject victim)
        {
            if (victim.GetComponent<IsKilled>() != null)
                return;
            
            var isKilled = victim.AddComponent<IsKilled>();
            isKilled.SetKiller(killer);
            
            OnKill(killer, victim);
        }
    }
}