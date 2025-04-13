using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Common.Interfaces;
using Core.Interactables.Interfaces;
using Game.UI.Signals;
using Infrastructure.Input.Signals.Keys;
using Infrastructure.Localization;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Character.Player.Impls
{
    /// <summary>
    /// Place on ICharacter.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class PlayerFrontTriggerArea : MonoBehaviour
    {
        [SerializeField] private LayerMask triggeredByLayer;

        [Inject] private DiContainer _container;

        private ICharacter _colliderOwner;
        private bool _isInitialized;
        private IInteractable _currentInteractable = null;
        private readonly HashSet<IInteractable> _interactablesInTrigger = new();
        private SignalBus _signalBus;
        private ILocalizationProvider _localizationProvider;


        public void Init(ICharacter owner)
        {
            _signalBus = _container.Resolve<SignalBus>() ?? throw new NullReferenceException("SignalBus is null.");
            _localizationProvider = _container.Resolve<ILocalizationProvider>();

            _signalBus.Subscribe<InteractKeySignal>(OnInteractKeySignal);
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

            if (!IsLayerInMask(other.gameObject.layer)) return;

            var interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable == null) return;

            _interactablesInTrigger.Add(interactable);

            if (_interactablesInTrigger.Count > 1)
            {
                var names = string.Join(", ", _interactablesInTrigger.Select(i => ((Component)i).gameObject.name));
                throw new InvalidOperationException($"Multiple interactables in trigger zone.  [{names}]");
            }

            _currentInteractable = interactable;

            var position = other.transform.position;
            var promptPosition = new Vector3(position.x, 3f, position.z);

            var tip = GetInteractionTip(interactable);

            _signalBus.Fire(new ShowInteractTipSignal(tip, promptPosition));
        }

        private (string, string) GetInteractionTip(IInteractable interactable)
        {
            var name1 = _localizationProvider.Localize(interactable.LocalizationKey, WordTransform.Upper);
            var action = _localizationProvider.Localize(interactable.InteractionTipNameId, WordTransform.Upper);

            return (name1, action);
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
                _signalBus.Fire(new HideInteractTipSignal());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isInitialized) return;
            if (IsLayerInMask(other.gameObject.layer))
            {
            }
        }

        private void OnInteractKeySignal(InteractKeySignal signal)
        {
            if (!_colliderOwner.GetInteractor().IsBusy())
            {
                _currentInteractable?.Interact(_colliderOwner, OnInteractionComplete);
                _colliderOwner.GetInteractor().SetBusy(true);
            }
            else
            {
                Log.Warn("Impossible to interact. Character is busy.");
            }
        }

        private void OnInteractionComplete() => _colliderOwner.GetInteractor().SetBusy(false);
        private bool IsLayerInMask(int layer) => (triggeredByLayer.value & (1 << layer)) != 0;
    }
}
