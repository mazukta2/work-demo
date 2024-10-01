using System;
using System.Collections.Generic;
using Model.Animals.Eating;
using Model.Tags;
using UnityEngine;
using Utilities;

namespace Model.Animals.DeathCounting
{
    public class DeathCount : MonoBehaviour
    {
        [SerializeField] private KillingManager _animals;
        public event Action<ScriptableTag> OnChangedForTag = delegate { };
        
        private readonly Dictionary<ScriptableTag, int> _counters = new ();


        protected void OnEnable()
        {
            _animals.ThrowExceptionIfNull();
            _animals.OnKill += HandleOnAnimalEaten;
        }

        protected void OnDisable()
        {
            _animals.OnKill -= HandleOnAnimalEaten;
        }
        
        private void HandleOnAnimalEaten(GameObject killer, GameObject victim)
        {
            foreach (var victimTag in victim.GetComponents<Tag>())
            {
                AddKill(victimTag.GetTag());
            }
        }

        public int GetKillCount(ScriptableTag scriptableTag)
        {
            return _counters.GetValueOrDefault(scriptableTag, 0);
        }

        private void AddKill(ScriptableTag scriptableTag)
        {
            if (_counters.TryGetValue(scriptableTag, out var count))
                _counters[scriptableTag] = count + 1;
            else
                _counters.Add(scriptableTag, 1);

            OnChangedForTag(scriptableTag);
        }
    }
}