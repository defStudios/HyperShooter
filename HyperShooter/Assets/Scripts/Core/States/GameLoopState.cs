using System.Threading.Tasks;
using Core.Services;
using Projectiles;
using Core.Assets;
using Core.Input;
using Level;
using Player;
using UnityEngine;

namespace Core.States
{
    public class GameLoopState : IPayloadedState<LevelController>
    {
        private readonly StateMachine _stateMachine;

        private LevelController _level;
        private Projectile _projectile;

        public GameLoopState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(LevelController level)
        {
            _level = level;

            SubscribeOnPlayerEvents(level.Player);
            StartPlayerTurn();
        }
        
        public void Exit()
        {
            UnsubscribeFromPlayerEvents(_level.Player);
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
            await Task.Delay(_projectile.Data.InfectionDurationMilliseconds + 
                             _projectile.Data.InfectionPostDurationMilliseconds);
            
            TryStartPlayerMovementStage();
        }

        private async void OnProjectileMissed()
        {
            await Task.Delay(_projectile.Data.ProjectileMissTimeoutMilliseconds);
            TryStartPlayerMovementStage();
        }

        private void TryStartPlayerMovementStage()
        {
            if (_level.Player.Scale.OvercameMinimumScale)
            {
                ShowResult(false);
                return;
            }

            _level.Player.Movement.MoveTowardsDoors();
        }

        private async void OnPlayerMovementDone(bool reachedDoors)
        {
            if (reachedDoors)
            {
                await _level.Doors.Open();
                ShowResult(true);
            }
            else
                StartPlayerTurn();
        }

        private void ShowResult(bool win)
        {
            Debug.Log(win ? "Congratulations!" : "You are too small!");
            ReloadLevel();
        }
        
        private void ReloadLevel()
        {
            _stateMachine.Enter<LoadLevelState, LevelData>(
                ServiceManager.Container.Single<IAssetsDatabase>().Levels[0]);
        }

        private void SubscribeOnPlayerEvents(PlayerController player)
        {
            player.OnProjectileLaunched += OnProjectileLaunched;
            player.Movement.OnMovementDone += OnPlayerMovementDone;
        }

        private void UnsubscribeFromPlayerEvents(PlayerController player)
        {
            player.OnProjectileLaunched -= OnProjectileLaunched;
            player.Movement.OnMovementDone -= OnPlayerMovementDone;
        }
    }
}
