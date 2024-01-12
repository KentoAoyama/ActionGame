namespace Domain
{
    public interface IPlayerComponent
    {
        IBattleInput Input { get; }

        void Move();

        void LookRotationCameraDirMoveState();

        void LookRotationCameraDirIdleState();

        void Attack();

        void TransitionState(IPlayerState state);

        //void Initialized();

        //void Update();
    }
}