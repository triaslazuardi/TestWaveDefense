using UnityEngine;

namespace Nitzz.Game.Cores
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}