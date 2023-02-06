using Core.Services;
using Core.Workflow;
using System;

namespace Utils
{
    public class Timer : ITickable
    {
        private readonly float _duration;

        private bool _active;
        private float _timeSpent;
        private Action _onCompleted;
        
        public Timer(float duration, Action onCompleted)
        {
            _active = true;
            
            _duration = duration;
            _onCompleted = onCompleted;
            
            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_active)
                return;
            
            _timeSpent += deltaTime;

            if (_timeSpent > _duration)
            {
                _active = false;
                _onCompleted?.Invoke();
            }
        }

        public void OnDestroy()
        {
            ServiceManager.Container.Single<ITickRunner>().Unsubscribe(this);
        }

        public void Stop() => _active = false;
    }
}