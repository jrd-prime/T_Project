using System;
using Core.Character.Common;
using Game.UI.Impls;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Core.Character.Player.Impls
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

        [Inject] private SignalBus _signalBus;

        public void Init(ICharacter owner)
        {
            if (_signalBus == null) throw new NullReferenceException("SignalBus is null.");

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

            if (!IsLayerInMask(other.gameObject.layer) || other is not BoxCollider boxCollider) return;

            Log.Warn("trigger enter: " + other.name);
            var position = other.transform.position;
            var promptPosition = new Vector3(position.x, boxCollider.size.y + 0.5f, position.z);
            _signalBus.Fire(new ShowInteractPromptSignal("Press <b>E</b> to interact", promptPosition));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInitialized) return;

            if (!IsLayerInMask(other.gameObject.layer)) return;

            Log.Warn("trigger exit: " + other.name);
            _signalBus.Fire(new HideInteractPromptSignal());
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
