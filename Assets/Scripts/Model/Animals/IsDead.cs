using UnityEngine;

namespace Model.Animals
{
    // TODO: model logic don't have "IsDead" state, but this behaviour introduces it
    // We should encapsulate this logic inside killing manager.
    public class IsDead : MonoBehaviour
    {
        protected void LateUpdate()
        {
            Destroy(gameObject);
        }
    }
}