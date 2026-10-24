using UnityEngine;

namespace Nitzz.Game.Core
{
    public class StateMachine
    {
        public IState Current { get; private set; }

        public void ChangeState(IState next)
        {
            Current?.Exit();
            Current = next;
            Current.Enter();
        }

        public void Update() => Current?.Tick();
    }
}