using System.Threading.Tasks;
using Core.Services;
using Projectiles;
using Core.Assets;
using Core.Input;
using Player;
using UnityEngine;

namespace Core.States
{
    public class GameLoopState : IPayloadedState<PlayerController>
    {
        private readonly StateMachine _stateMachine;

        private PlayerController _player;
        private Projectile _projectile;

        public GameLoopState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(PlayerController player)
        {
            _player = player;

            SubscribeOnPlayerEvents(_player);
            StartPlayerTurn();
        }
        
        public void Exit()
        {
            // hide popups

            UnsubscribeFromPlayerEvents();
            ServiceManager.Container.Single<IInput>().StopListening();
        }

        private void StartPlayerTurn()
        {
            ServiceManager.Container.Single<IInput>().StartListening();
        }

        private void OnProjectileLaunched(Projectile projectile)
        {
            ServiceManager.Container.Single<IInput>().StopListening();

            _projectile = projectile;
            _projectile.OnProjectileMissed += OnProjectileMissed;
            _projectile.OnObstaclesInfected += OnObstaclesInfected;
        }

        private async void OnObstaclesInfected()
        {
            await Task.Delay(_projectile.Data.InfectionDurationMilliseconds + 100);
            StartPlayerMovementStage();
        }

        private void OnProjectileMissed() => StartPlayerMovementStage();

        private void StartPlayerMovementStage()
        {
            _player.StartMovementStage();
        }

        private void OnPlayerOvercameMinimumScale()
        {
            // show lose popup
            Debug.Log("You are too small!");
            ReloadLevel();
            
        }

        private async void OnPlayerMovementDone(bool reachedDoors)
        {
            Debug.Log("Movement done!");
            // if player is close, show win popup
            // else StartPlayerTurn();
        }
        
        private void ReloadLevel()
        {
            _stateMachine.Enter<LoadLevelState, LevelData>(ServiceManager.Container.Single<IAssetsDatabase>().Levels[0]);
        }

        private void SubscribeOnPlayerEvents(PlayerController player)
        {
            player.OnProjectileLaunched += OnProjectileLaunched;
            player.OnPlayerOvercameMinimumScale += OnPlayerOvercameMinimumScale;
            player.OnMovementDone += OnPlayerMovementDone;
        }

        private void UnsubscribeFromPlayerEvents()
        {
            _player.OnProjectileLaunched -= OnProjectileLaunched;
            _player.OnPlayerOvercameMinimumScale -= OnPlayerOvercameMinimumScale;
            _player.OnMovementDone -= OnPlayerMovementDone;
        }
    }
}
