using Core.Character.Common;
using Game.UI;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Core.Character.Player.Interactors
{
    /// <summary>
    /// Place on ICharacter.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class FrontTriggerArea : MonoBehaviour
    {
        [SerializeField] private LayerMask triggeredByLayer;

        private ICharacter _colliderOwner;
        private bool _isInitialized;

        [Inject] private SignalBus signalBus;

        public void Init(ICharacter owner)
        {
            Log.Warn("_signalBus: " + signalBus);
            _colliderOwner = owner;
            _isInitialized = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized)
            {
                Log.Error("Not initialized. Use Init(). " + name);
                return;
            }

            if (IsLayerInMask(other.gameObject.layer))
            {
                Log.Warn("trigger enter: " + other.name);
                var n = new Vector3(other.transform.position.x, (other as BoxCollider).size.y + 0.5f,
                    other.transform.position.z);
                signalBus.Fire(new ShowInteractPromptSignal("Press <b>E</b> to interact", n));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInitialized) return;
            if (IsLayerInMask(other.gameObject.layer))
            {
                Log.Warn("trigger exit: " + other.name);

                signalBus.Fire(new HideInteractPromptSignal());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isInitialized) return;
            if (IsLayerInMask(other.gameObject.layer))
            {
            }
        }

        private bool IsLayerInMask(int layer) => (triggeredByLayer.value & (1 << layer)) != 0;
    }
}
