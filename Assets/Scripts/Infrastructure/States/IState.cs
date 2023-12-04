namespace Infrastructure.States
{
    public interface IExcitableState
    {
        void Exit();
    }

    public interface IState : IExcitableState
    {
        void Enter();
    }
}