using Core.Services;

namespace Core.Workflow
{
    public interface IFixedTickRunner : ISingleService
    {
        public void Subscribe(IFixedTickable fixedTickable);
        public void Unsubscribe(IFixedTickable fixedTickable);
    }
}