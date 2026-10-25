using Nitzz.Game.Cores;
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

        public void Enter()
        {
            GameEvents.RaisePhaseChanged("DEFEND");
            gm.BeginWave();
        }

        public void Tick() => gm.TickDefend();
        public void Exit() { }
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