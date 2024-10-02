using Infrastructure.GameEvents;
using Model.Tags;
using TMPro;
using UnityEngine;
using Utilities;

namespace Model.Animals.DeathCounting
{
    public class DeathCountAsText : MonoBehaviour
    {
        [SerializeField] private ScriptableTag _tag;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private DeathCount _deathCount;
        [SerializeField] private GameEvent<DeathCount.OnDeathCountChangedEvent> _onDeathCountChanged;
        
        public void Receive(DeathCount value)
        {
            _deathCount = value.ThrowExceptionIfNull();
        }
        
        protected void OnEnable()
        {
            _tag.ThrowExceptionIfNull();
            _value.ThrowExceptionIfNull();
            _deathCount.ThrowExceptionIfNull();
            _onDeathCountChanged.OnEvent += HandleChanged;
            UpdateView();
        }

        protected void OnDisable()
        {
            _onDeathCountChanged.OnEvent -= HandleChanged;
        }

        private void HandleChanged(DeathCount.OnDeathCountChangedEvent message)
        {
            if (_deathCount == message.DeathCount && message.Tag == _tag)
                UpdateView();
        }
        
        private void UpdateView()
        {
            _value.text = _deathCount.GetKillCount(_tag).ToString();
        }
    }
}