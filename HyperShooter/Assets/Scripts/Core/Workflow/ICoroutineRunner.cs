using System.Collections;
using UnityEngine;

namespace Core.Workflow
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
