using System;
using System.Collections;
using Code.Core.WORK.Pools;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Code.Core.WORK.UI.PopUpText
{
    public class PopUpTextManager : MonoBehaviour
    {
        [FormerlySerializedAs("popUpTextHolderPrefab")] [SerializeField]
        private PopUpTextHolderView popUpTextHolderViewPrefab;

        private CustomPool<PopUpTextHolderView> _popUpTextHolderPool;
        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        private void Awake()
        {
            if (_resolver == null) throw new NullReferenceException("Resolver is null. Add this to gamecontext.");
            if (popUpTextHolderViewPrefab == null)
                throw new NullReferenceException("PopUpTextHolderPrefab is not set.");

            _popUpTextHolderPool =
                new CustomPool<PopUpTextHolderView>(popUpTextHolderViewPrefab, 10, transform, _resolver);
        }

        public void ShowPopUpText(string text, Vector3 position, float durationSeconds, bool isCrit)
        {
            var popUpTextHolder = _popUpTextHolderPool.Get();

            popUpTextHolder.transform.position = position;

            popUpTextHolder.gameObject.SetActive(true);

            popUpTextHolder.Show(text, durationSeconds, isCrit);
            popUpTextHolder.StartCoroutine(PopUpDuration(popUpTextHolder, durationSeconds));
        }

        private IEnumerator PopUpDuration(PopUpTextHolderView popUpTextHolderView, float durationSeconds)
        {
            yield return new WaitForSeconds(durationSeconds);
            _popUpTextHolderPool.Return(popUpTextHolderView);
        }
    }
}
