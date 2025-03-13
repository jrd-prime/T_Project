using System;
using System.Collections.Generic;
using Code.Core.WORK.Enums.States;
using Code.Core.WORK.UI.Base.Data;
using UnityEngine;

namespace Code.Core.WORK.UI.Base.View
{
    public abstract class UIViewBase : MonoBehaviour, IUIView
    {
        [SerializeField] public EGameState viewForEGameState = EGameState.NotSet;

        protected readonly Dictionary<Enum, JSubViewBase> SubViewsCache = new();

        private void Awake()
        {
            if (viewForEGameState == EGameState.NotSet)
                throw new Exception("GameStateType for view is not set. " + name);
        }

        public SubViewDto GetSubViewDto(Enum subState)
        {
            var subView = GetSubView(subState) ?? throw new NullReferenceException("SubView is null. " + subState);

            return new SubViewDto
            {
                InSafeZone = subView.inSafeZone,
                Template = subView.GetTemplate()
            };
        }

        private JSubViewBase GetSubView(Enum subState)
        {
            CheckSubViewsCount(subState);


            if (!SubViewsCache.TryGetValue(subState, out var subViewBase))
                throw new KeyNotFoundException($"SubView not found in cache for: {subState}");

            return subViewBase;
        }

        private void CheckSubViewsCount(Enum subState)
        {
            if (SubViewsCache.Count == Enum.GetNames(subState.GetType()).Length) return;

            Debug.LogError("--- SubView cache ---");
            Debug.LogError(
                $"SubView count({SubViewsCache.Count}) is not equal to game sub state count({Enum.GetNames(subState.GetType()).Length})");

            foreach (var subView in SubViewsCache)
            {
                Debug.LogError($"SubView in cache: {subView.Key} / {subView.Value}");
            }

            Debug.LogError("Create sub view for sub state: " + subState);
            Debug.LogError("--- SubView cache ---");
        }
    }
}
