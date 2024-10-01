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
        
        public void Receive(DeathCount value)
        {
            _deathCount = value.ThrowExceptionIfNull();
        }
        
        protected void OnEnable()
        {
            _tag.ThrowExceptionIfNull();
            _value.ThrowExceptionIfNull();
            _deathCount.ThrowExceptionIfNull();
            _deathCount.OnChangedForTag += HandleChanged;
            UpdateView();
        }

        protected void OnDisable()
        {
            _deathCount.OnChangedForTag -= HandleChanged;
        }

        private void HandleChanged(ScriptableTag scriptableTag)
        {
            if (scriptableTag == _tag)
                UpdateView();
        }
        
        private void UpdateView()
        {
            _value.text = _deathCount.GetKillCount(_tag).ToString();
        }
    }
}