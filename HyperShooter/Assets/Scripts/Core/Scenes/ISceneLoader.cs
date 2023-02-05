using Core.Services;
using System;

namespace Core.Scenes
{
    public interface ISceneLoader : ISingleService
    {
        public void Load(string name, Action onLoaded = null);
    }
}