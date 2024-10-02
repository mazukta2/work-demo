using System;
using Infrastructure.GameEvents;
using UnityEngine;

namespace Model.Animals.Eating
{
    public class Predator : MonoBehaviour
    {
        [SerializeField] private GameEvent<OnPredatorKilledEvent> _onKill;
        
        public void Kill(GameObject victim)
        {
            if (victim.GetComponent<IsDead>() != null)
                return;
            
            victim.AddComponent<IsDead>();
            
            _onKill.Invoke(new OnPredatorKilledEvent(gameObject, victim));
        }
        
        public readonly struct OnPredatorKilledEvent 
        {
            public readonly GameObject Killer;
            public readonly GameObject Victim;

            public OnPredatorKilledEvent(GameObject killer, GameObject victim)
            {
                Victim = victim;
                Killer = killer;
            }
        }
    }
}