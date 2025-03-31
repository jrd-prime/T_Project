// using System;
// using System.Collections.Generic;
// using Core.FSM.Data;
// using Game.UI._old.Base.Data;
// using Game.UI.Interfaces;
// using UnityEngine;
//
// namespace Game.UI._old.Base.View
// {
//     public abstract class ViewBase : MonoBehaviour, IUIView
//     {
//         [SerializeField] public GameStateType viewForGameStateType = GameStateType.NotSet;
//
//         protected readonly Dictionary<Enum, SubViewBase> SubViewsCache = new();
//
//         private void Awake()
//         {
//             if (viewForGameStateType == GameStateType.NotSet)
//                 throw new Exception("GameStateType for view is not set. " + name);
//         }
//
//         public SubViewTemplateData GetSubViewTemplateData(Enum subState)
//         {
//             var subView = GetSubView(subState) ?? throw new NullReferenceException("SubView is null. " + subState);
//
//             return new SubViewTemplateData
//             {
//                 InSafeZone = subView.inSafeZone,
//                 Template = subView.GetTemplate()
//             };
//         }
//
//         private SubViewBase GetSubView(Enum subState)
//         {
//             CheckSubViewsCount(subState);
//
//             if (!SubViewsCache.TryGetValue(subState, out var subViewBase))
//                 throw new KeyNotFoundException($"SubView not found in cache for: {subState}");
//
//             return subViewBase;
//         }
//
//         private void CheckSubViewsCount(Enum subState)
//         {
//             if (SubViewsCache.Count == Enum.GetNames(subState.GetType()).Length) return;
//
//             Debug.LogWarning(
//                 $"--- SubView cache --- / SubView count({SubViewsCache.Count}) is not equal to game sub state count({Enum.GetNames(subState.GetType()).Length})");
//
//             foreach (var subView in SubViewsCache)
//             {
//                 Debug.LogWarning($"SubView in cache: {subView.Key} / {subView.Value}");
//             }
//
//             Debug.LogWarning("Create sub view for sub state: " + subState);
//             Debug.LogWarning("--- SubView cache ---");
//         }
//
//         public void Show()
//         {
//         }
//
//         public void Hide()
//         {
//         }
//     }
// }
