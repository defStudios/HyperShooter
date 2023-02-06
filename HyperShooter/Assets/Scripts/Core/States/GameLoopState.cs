using Core.Services;
using Core.Assets;
using Core.Input;

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
            // subscribe on events
            
            StartPlayerTurn();
        }
        
        public void Exit()
        {
            // hide popups
            // unsubscribe from events
            
            ServiceManager.Container.Single<IInput>().StopListening();
        }

        private void StartPlayerTurn()
        {
            ServiceManager.Container.Single<IInput>().StartListening();
        }

        private void OnProjectileLaunched()
        {
            ServiceManager.Container.Single<IInput>().StopListening();
        }

        private void OnObstaclesDestroyed() => CheckPlayerAndTrajectory();
        private void OnProjectileMissed() => CheckPlayerAndTrajectory();

        private void CheckPlayerAndTrajectory()
        {
            
        }

        private void OnPlayerMovementDone()
        {
            // check distance to doors
            // if player is close, show win popup
            // else StartPlayerTurn();
        }

        private void ReloadLevel()
        {
            _stateMachine.Enter<LoadLevelState, LevelData>(ServiceManager.Container.Single<IAssetsDatabase>().Levels[0]);
        }
    }
}
