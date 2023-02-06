using Core.Services;
using System;

namespace Core.Input
{
    public interface IInput : ISingleService
    {
        public Action OnTapBegun { get; set; }
        public Action OnTapping { get; set; }
        public Action OnTapEnded { get; set; }

        public void StartListening();
        public void StopListening();
    }
}
