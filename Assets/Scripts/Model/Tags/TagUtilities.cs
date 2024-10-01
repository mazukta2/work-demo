using UnityEngine;

namespace Model.Tags
{
    public class TagUtilities
    {
        public static bool IsTaggedAs(GameObject gameObject, ScriptableTag scriptableTag)
        {
            foreach (var tag in gameObject.GetComponents<Tag>())
            {
                if (tag.IsTaggedAs(scriptableTag))
                    return true;
            }

            return false;
        }
    }
}