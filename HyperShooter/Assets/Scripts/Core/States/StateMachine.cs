using System.Collections.Generic;
using System;
using Core.Assets;
using Core.Factories;
using Core.Services;
using Core.Scenes;

namespace Core.States
{
    public class StateMachine
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ServiceManager _services;
        
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public StateMachine(ISceneLoader sceneLoader, IAssetsDatabase assetsDatabase, ServiceManager services)
        {
            _sceneLoader = sceneLoader;
            _services = services;
            
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, assetsDatabase, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this,services.Single<IGameFactory>(), services.Single<IAssetsDatabase>(), services),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        
        public void Enter<TState>() where TState: class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }	
        
        private TState ChangeState<TState>() where TState: class, IState
        {
            _activeState?.Exit();
			
            var state = _states[typeof(TState)] as TState;
            _activeState = state;

            return state;
        }
    }
}
