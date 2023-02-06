namespace Core.Workflow
{
    public interface IFixedTickable
    {
        public void FixedTick(float deltaTime);
        public void OnDestroy();
    }
}