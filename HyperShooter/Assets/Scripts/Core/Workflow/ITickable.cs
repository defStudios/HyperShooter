namespace Core.Workflow
{
    public interface ITickable
    {
        public void Tick(float deltaTime);
        public void OnDestroy();
    }
}