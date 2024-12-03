using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class Enemy : MonoBehaviour
{
    public IState currentState;

    private void Start()
    {
        TransitionToState(new IdleState(this));
    }

    private void Update()
    {
        currentState.Update();
        Debug.Log(currentState);
    }

    public void TransitionToState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }
}

public class IdleState : IState
{
    private Enemy enemy;

    public IdleState(Enemy enemy)
    {
        this.enemy = enemy;

        
    }

    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
