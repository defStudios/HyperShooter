using System.Collections;
using System;
using Core.Workflow;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Core.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _runner;

        public SceneLoader(ICoroutineRunner runner)
        {
            _runner = runner;
        }

        public void Load(string name, Action onLoaded = null) =>
            _runner.StartCoroutine(SceneLoading(name, onLoaded));

        private IEnumerator SceneLoading(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
            {
                onLoaded?.Invoke();
                yield break;
            }
			
            AsyncOperation waitForLoad = SceneManager.LoadSceneAsync(name);

            while (!waitForLoad.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
