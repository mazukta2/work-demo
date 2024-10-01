using UnityEngine;

namespace Model.Tags
{
    public class Tag : MonoBehaviour
    {
        [SerializeField] private ScriptableTag _tag;

        public bool IsTaggedAs(ScriptableTag scriptableTag)
        {
            return _tag == scriptableTag;
        }
        
        public ScriptableTag GetTag()
        {
            return _tag;
        }
    }
}