using Core.Services;
using Core.Workflow;
using System;
using UnityInput = UnityEngine.Input;

namespace Core.Input
{
    public class InputController : IInput, ITickable
    {
        public Action OnTapBegun { get; set; }
        public Action OnTapping { get; set; }
        public Action OnTapEnded { get; set; }
        
        private bool _listening;
        private bool _tapping;

        public InputController()
        {
            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_listening)
                return;

            if (UnityInput.GetMouseButtonDown(0))
            {
                _tapping = true;
                OnTapBegun.Invoke();
            }
            else if (UnityInput.GetMouseButton(0) && _tapping)
            {
                OnTapping.Invoke();
            }
            else if (UnityInput.GetMouseButtonUp(0) && _tapping)
            {
                _tapping = false;
                OnTapEnded.Invoke();
            }
        }

        public void StartListening()
        {
            _listening = true;
        }

        public void StopListening()
        {
            _listening = false;

            if (_tapping)
            {
                _tapping = false;
                OnTapEnded.Invoke();
            }
        }
    }
}
