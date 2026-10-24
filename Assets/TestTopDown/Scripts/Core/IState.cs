using UnityEngine;

namespace Nitzz.Game.Core
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}