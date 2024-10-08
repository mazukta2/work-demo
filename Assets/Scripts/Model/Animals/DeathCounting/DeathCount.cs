﻿using System;
using System.Collections.Generic;
using Infrastructure.GameEvents;
using Model.Animals.Eating;
using Model.Tags;
using UnityEngine;
using Utilities;

namespace Model.Animals.DeathCounting
{
    public class DeathCount : MonoBehaviour
    {
        [SerializeField] private GameEvent<Predator.OnPredatorKilledEvent> _onKill;
        [SerializeField] private GameEvent<OnDeathCountChangedEvent> _onDeathCountChanged;
        
        private readonly Dictionary<ScriptableTag, int> _counters = new ();

        protected void OnEnable()
        {
            _onKill.OnEvent += HandleOnAnimalEaten;
        }

        protected void OnDisable()
        {
            _onKill.OnEvent -= HandleOnAnimalEaten;
        }
        
        private void HandleOnAnimalEaten(Predator.OnPredatorKilledEvent message)
        {
            foreach (var victimTag in message.Victim.GetComponents<Tag>())
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
            var value = 0;
            if (_counters.TryGetValue(scriptableTag, out var count))
            {
                _counters[scriptableTag] = count + 1;
                value = count + 1;
            }
            else
            {
                _counters.Add(scriptableTag, 1);
                value = 1;
            }

            _onDeathCountChanged.Invoke(new OnDeathCountChangedEvent(this, scriptableTag, value));
        }
        
        public readonly struct OnDeathCountChangedEvent
        {
            public readonly DeathCount DeathCount;
            public readonly ScriptableTag Tag;
            public readonly int Count;

            public OnDeathCountChangedEvent(DeathCount deathCount, ScriptableTag tag, int count)
            {
                DeathCount = deathCount;
                Tag = tag;
                Count = count;
            }
        }
    }
}