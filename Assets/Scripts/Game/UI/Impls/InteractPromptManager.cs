using UnityEngine;
using Zenject;

namespace Game.UI.Impls
{
    public sealed class InteractPromptManager : MonoBehaviour
    {
        [SerializeField] private WorldSpaceUI _uiPromptPrefab;

        [Inject] private SignalBus _signalBus;

        private WorldSpaceUI _uiInstance;
        private Quaternion _initialRotation;
        private const string LabelName = "text";

        private void Awake()
        {
            _initialRotation = _uiPromptPrefab.transform.rotation;
            _uiInstance = _uiPromptPrefab;
            _uiInstance.gameObject.SetActive(false);
            _signalBus.Subscribe<ShowInteractPromptSignal>(ShowPrompt);
            _signalBus.Subscribe<HideInteractPromptSignal>(HidePrompt);
        }

        private void ShowPrompt(ShowInteractPromptSignal signal)
        {
            _uiInstance.gameObject.SetActive(true);
            _uiInstance.transform.SetPositionAndRotation(signal.WorldPosition, _initialRotation);

            _uiInstance.SetLabelText(LabelName, signal.Text);
        }

        private void HidePrompt(HideInteractPromptSignal signal) => _uiInstance.gameObject.SetActive(false);

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ShowInteractPromptSignal>(ShowPrompt);
            _signalBus.Unsubscribe<HideInteractPromptSignal>(HidePrompt);
        }
    }
}
