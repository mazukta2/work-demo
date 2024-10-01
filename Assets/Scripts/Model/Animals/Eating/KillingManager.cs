using System;
using UnityEngine;

namespace Model.Animals.Eating
{
    // TODO: Prey and predator counts are hardcoded. Consider using ScriptableObject tags, to set it in editor.
    public class KillingManager : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnKill = delegate { };

        public void Kill(GameObject killer, GameObject victim)
        {
            if (victim.GetComponent<IsDead>() != null)
                return;
            
            victim.AddComponent<IsDead>();
            
            OnKill(killer, victim);
        }
    }
}