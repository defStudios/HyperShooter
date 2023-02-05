using System.Collections;
using UnityEngine;

namespace Core.Utils
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
