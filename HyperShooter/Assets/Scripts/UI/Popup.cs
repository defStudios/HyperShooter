using UnityEngine;
using System;

namespace UI
{
    public class Popup : MonoBehaviour
    {
        private Action _onClosed;
        
        public void Show(Action onClosed = null)
        {
            _onClosed = onClosed;
        }

        public void Close()
        {
            _onClosed.Invoke();
            Destroy(gameObject);
        }
    }
}
