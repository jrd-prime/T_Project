using Game.UI.Impls;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public sealed class InteractPromptManager : MonoBehaviour
    {
        [SerializeField] private WorldSpaceUI _uiPromptPrefab;
        private WorldSpaceUI promptInstance;

        [Inject] private SignalBus signalBus;
        private Quaternion initialRotation;
        private const string labelName = "text";

        private void Awake()
        {
            initialRotation = _uiPromptPrefab.transform.rotation;
            promptInstance = _uiPromptPrefab;
            promptInstance.gameObject.SetActive(false);
            signalBus.Subscribe<ShowInteractPromptSignal>(ShowPrompt);
            signalBus.Subscribe<HideInteractPromptSignal>(HidePrompt);
        }

        private void HidePrompt(HideInteractPromptSignal signal) => promptInstance.gameObject.SetActive(false);

        private void ShowPrompt(ShowInteractPromptSignal signal)
        {
            Log.Warn("signal: " + signal.Text + " " + signal.WorldPosition + " / initialRotation: " + initialRotation);
            promptInstance.gameObject.SetActive(true);
            promptInstance.transform.SetPositionAndRotation(signal.WorldPosition, initialRotation);

            promptInstance.SetLabelText(labelName, signal.Text);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<ShowInteractPromptSignal>(ShowPrompt);
            signalBus.Unsubscribe<HideInteractPromptSignal>(HidePrompt);
        }
    }

    public record HideInteractPromptSignal
    {
    }

    public record ShowInteractPromptSignal(string Text, Vector3 WorldPosition)
    {
        public string Text { get; } = Text;
        public Vector3 WorldPosition { get; } = WorldPosition;
    }
}
