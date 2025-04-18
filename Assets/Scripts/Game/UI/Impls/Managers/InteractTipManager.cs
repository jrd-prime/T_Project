﻿using System;
using Game.UI.Impls.Views.WorldTips;
using Game.UI.Signals;
using UnityEngine;
using Zenject;

namespace Game.UI.Impls.Managers
{
    public sealed class InteractTipManager : MonoBehaviour
    {
        [SerializeField] private InteractablesTipUI interactablesTipUI;

        [Inject] private SignalBus _signalBus;

        private Quaternion _initialRotation;

        private void Awake()
        {
            if (!interactablesTipUI)
                throw new NullReferenceException($"{interactablesTipUI.GetType()} not added. {name}");

            _initialRotation = interactablesTipUI.transform.rotation;
            interactablesTipUI.gameObject.SetActive(false);
            _signalBus.Subscribe<ShowInteractTipSignal>(ShowTip);
            _signalBus.Subscribe<HideInteractTipSignal>(HideTip);
        }

        private void ShowTip(ShowInteractTipSignal signal)
        {
            interactablesTipUI.gameObject.SetActive(true);
            interactablesTipUI.transform.SetPositionAndRotation(signal.WorldPosition, _initialRotation);

            interactablesTipUI.SetTipText(signal.TextTuple);
        }

        private void HideTip(HideInteractTipSignal signal) => interactablesTipUI.gameObject.SetActive(false);

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShowInteractTipSignal>(ShowTip);
            _signalBus.Unsubscribe<HideInteractTipSignal>(HideTip);
        }
    }
}
