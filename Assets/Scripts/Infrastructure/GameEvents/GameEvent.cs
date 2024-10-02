using System;
using Model.Animals.Eating;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.GameEvents
{
    [Serializable]
    public struct GameEvent<T>
    {
        public event Action<T> OnEvent
        {
            add => OnEventStatic += value;
            remove => OnEventStatic -= value;
        }
        
        private static event Action<T> OnEventStatic;

        public void Invoke(T message)
        {   
            OnEventStatic?.Invoke(message);
        }

    }
}