using ModestTree;
using UnityEngine;

namespace Game.Gameplay.Interactables
{
    [RequireComponent(typeof(Collider))]
    public sealed class InteractableArea : MonoBehaviour
    {
        public Collider AreaCollider => GetComponent<Collider>();

        private void Awake() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            Log.Warn("on trigger enter: " + other.name);
        }

        private void OnTriggerStay(Collider other)
        {
            Log.Warn("on trigger stay: " + other.name);
        }

        private void OnTriggerExit(Collider other)
        {
            Log.Warn("on trigger exit: " + other.name);
        }
    }
}
