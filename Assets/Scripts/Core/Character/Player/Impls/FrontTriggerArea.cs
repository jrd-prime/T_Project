using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Common;
using Core.Interactables.Interfaces;
using Game.UI.Impls;
using Infrastructure.Input;
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

        private IInteractable _currentInteractable = null;

        public void Init(ICharacter owner)
        {
            if (_signalBus == null) throw new NullReferenceException("SignalBus is null.");
            _signalBus.Subscribe<InteractKeySignal>(OnInteractKeySignal);
            _colliderOwner = owner;
            _isInitialized = true;
        }

        private void OnInteractKeySignal(InteractKeySignal signal)
        {
            _currentInteractable?.Interact();
        }

        private readonly HashSet<IInteractable> _interactablesInTrigger = new();

        private void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized)
            {
                Log.Error("Not initialized. Use Init(). " + name);
                return;
            }

            if (!IsLayerInMask(other.gameObject.layer)) return;

            var interactable = other.gameObject.GetComponent<IInteractable>();
            Log.Warn("other: " + other.name);
            if (interactable == null) return;

            _interactablesInTrigger.Add(interactable);

            if (_interactablesInTrigger.Count > 1)
            {
                var names = string.Join(", ", _interactablesInTrigger.Select(i => ((Component)i).gameObject.name));
                Log.Error($"More than one interactable in trigger at once! [{names}]");
                throw new InvalidOperationException("Multiple interactables in trigger zone.");
            }

            _currentInteractable = interactable;

            Log.Warn("trigger enter: " + other.name);
            var position = other.transform.position;
            var promptPosition = new Vector3(position.x, 3f, position.z);
            _signalBus.Fire(new ShowInteractPromptSignal("Press <b>E</b> to interact", promptPosition));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInitialized) return;
            if (!IsLayerInMask(other.gameObject.layer)) return;

            var interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable == null) return;

            _interactablesInTrigger.Remove(interactable);

            if (interactable == _currentInteractable)
            {
                _currentInteractable = null;
                Log.Warn("trigger exit: " + other.name);
                _signalBus.Fire(new HideInteractPromptSignal());
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
