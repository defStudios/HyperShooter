using UnityEngine;

namespace Core.States
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}
