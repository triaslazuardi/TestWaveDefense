using Nitzz.Game.Build;
using Nitzz.Game.Core;
using UnityEngine;


namespace Nitzz.Game.State
{
    public class BuildState : IState
    {
        public void Enter() => GameEvents.RaisePhaseChanged("BUILD");
        public void Tick() { }
        public void Exit() { }
    }

    public class DefendState : IState
    {
        readonly GameManager gm;
        public DefendState(GameManager gm) { this.gm = gm; }

        public void Enter() => GameEvents.RaisePhaseChanged("DEFEND");
        public void Exit() { }

        public void Tick()
        {
            // SEMENTARA (hari 1): Space = simulasi wave selesai.
            // Hari 2 diganti: wave selesai saat semua musuh mati.
            if (Input.GetKeyDown(KeyCode.Space)) gm.CompleteWave();
        }
    }

    public class ResultState : IState
    {
        readonly bool won;
        public ResultState(bool won) { this.won = won; }

        public void Enter() => GameEvents.RaisePhaseChanged(won ? "VICTORY" : "DEFEAT");
        public void Tick() { }
        public void Exit() { }
    }
}