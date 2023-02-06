using Core.Services;

namespace Core.Workflow
{
    public interface ITickRunner : ISingleService
    {
        public void Subscribe(ITickable tickable);
    }
}