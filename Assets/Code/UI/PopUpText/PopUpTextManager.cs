using System;
using System.Collections;
using Code.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.UI.PopUpText
{
    public class PopUpTextManager : MonoBehaviour
    {
        [FormerlySerializedAs("popUpTextHolderPrefab")] [SerializeField]
        private PopUpTextHolderView popUpTextHolderViewPrefab;

        private CustomPool<PopUpTextHolderView> _popUpTextHolderPool;
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container) => _container = container;

        private void Awake()
        {
            if (_container == null) throw new NullReferenceException("Container is null. Add this to gamecontext.");
            if (popUpTextHolderViewPrefab == null)
                throw new NullReferenceException("PopUpTextHolderPrefab is not set.");

            _popUpTextHolderPool =
                new CustomPool<PopUpTextHolderView>(popUpTextHolderViewPrefab, 10, transform, _container);
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
