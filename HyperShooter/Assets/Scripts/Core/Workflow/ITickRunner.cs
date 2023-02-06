using Core.Services;

namespace Core.Workflow
{
    public interface ITickRunner : ISingleService
    {
        public void Subscribe(ITickable tickable);
        public void Unsubscribe(ITickable tickable);
    }
}