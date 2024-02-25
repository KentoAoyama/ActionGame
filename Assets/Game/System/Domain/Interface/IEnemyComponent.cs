namespace Domain
{
    public interface IEnemyComponent
    {
        void Initialized();

        void Disabled();

        UnityEngine.GameObject GetGameObject();
    }
}
