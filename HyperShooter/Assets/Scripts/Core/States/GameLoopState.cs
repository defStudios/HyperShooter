using UnityEngine;

namespace Core.States
{
    public class GameLoopState : IState
    {
        private readonly StateMachine _stateMachine;

        public GameLoopState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }
        
        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}
