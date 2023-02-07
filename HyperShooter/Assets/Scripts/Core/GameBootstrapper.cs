using System.Collections.Generic;
using Core.Workflow;
using Core.Assets;
using UnityEngine;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner, ITickRunner, IFixedTickRunner
    {
        [SerializeField] private AssetsDatabase assetsDatabase;
        
        private Game _game;

        private List<ITickable> _tickables = new();
        private List<IFixedTickable> _fixedTickables = new();
        
        private void Start()
        {
            #if !UNITY_EDITOR
            Application.targetFrameRate = 60;
            #endif
            
            _game = new Game(this, this, this, assetsDatabase);
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedTickables.Count; i++)
                _fixedTickables[i].FixedTick(Time.fixedDeltaTime);
        }

        public void Subscribe(ITickable tickable)
        {
            if (!_tickables.Contains(tickable))
                _tickables.Add(tickable);
        }

        public void Unsubscribe(ITickable tickable) =>
            _tickables.Remove(tickable);

        public void Subscribe(IFixedTickable fixedTickable)
        {
            if (!_fixedTickables.Contains(fixedTickable))
                _fixedTickables.Add(fixedTickable);
        }

        public void Unsubscribe(IFixedTickable fixedTickable) =>
            _fixedTickables.Remove(fixedTickable);
    }
}
