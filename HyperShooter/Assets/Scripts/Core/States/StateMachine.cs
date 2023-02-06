using System.Collections.Generic;
using System;
using Core.Services;
using Core.Workflow;
using Core.Assets;
using Core.Scenes;

namespace Core.States
{
    public class StateMachine
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ServiceManager _services;
        
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public StateMachine(ITickRunner tickRunner, IFixedTickRunner fixedTickRunner, ISceneLoader sceneLoader, 
            IAssetsDatabase assetsDatabase, ServiceManager services)
        {
            _sceneLoader = sceneLoader;
            _services = services;
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, tickRunner, fixedTickRunner, sceneLoader, assetsDatabase, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, services),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        
        public void Enter<TState>() where TState: class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState: class, IExitableState
        {
            _activeState?.Exit();
			
            var state = _states[typeof(TState)] as TState;
            _activeState = state;

            return state;
        }
    }
}
