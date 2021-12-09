public class FiniteStateMachine
{
    public State currentState { get; private set; }
    public State previousState { get; private set; }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
