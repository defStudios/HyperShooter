using Core.Assets;
using UnityEngine;
using Core.Utils;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private AssetsDatabase assetsDatabase;
        
        private Game _game;
        
        private void Start()
        {
            _game = new Game(this, assetsDatabase);
            DontDestroyOnLoad(gameObject);
        }
    }
}
