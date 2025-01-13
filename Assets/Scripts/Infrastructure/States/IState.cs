public interface IState : IExitableState
{
    void Enter();
}

public interface IPayloadedState<TPayload> : IState
{
    void Enter(TPayload payload);
}

public interface IExitableState
{
    void Exit();
}
