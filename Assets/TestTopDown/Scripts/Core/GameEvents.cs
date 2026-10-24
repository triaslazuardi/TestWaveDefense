using System;
using UnityEngine;

namespace Nitzz.Game.Core
{
    public static class GameEvents
    {
        public static event Action<int> OnGoldChanged;
        public static event Action<int> OnWaveChanged;
        public static event Action<string> OnPhaseChanged;
        public static event Action OnCoreDestroyed;

        public static void RaiseGoldChanged(int gold) => OnGoldChanged?.Invoke(gold);
        public static void RaiseWaveChanged(int wave) => OnWaveChanged?.Invoke(wave);
        public static void RaisePhaseChanged(string phase) => OnPhaseChanged?.Invoke(phase);
        public static void RaiseCoreDestroyed() => OnCoreDestroyed?.Invoke();
    }
}