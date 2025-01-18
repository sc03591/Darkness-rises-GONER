using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
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
    public float speed = 1f;
    public float changeDirectionInterval = 2f;
    public float DirectionCooldown = 1f;
    public float radius = 12f; // Radius from the spawn point
    private Vector2 randomDirection;
    private Vector2 spawnPoint;
    public bool DebugRadius = true;
    public float DebugRadiusValue = 5f;
    

    private void Start()
    {
        spawnPoint = transform.position;
        TransitionToState(new IdleState(this));
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        currentState.Update();
        Vector2 currentPosition = transform.position;
        if (Vector2.Distance(currentPosition, spawnPoint) > radius)
        {
            DebugRadius = false;
            randomDirection = (spawnPoint - currentPosition).normalized;
        }
        if (Vector2.Distance(currentPosition, spawnPoint) < DebugRadiusValue)
        {
            DebugRadius = true;
        }
        transform.position += (Vector3)randomDirection * speed * Time.deltaTime;
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

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            if (DebugRadius)
            {
                NullDirection();
                yield return new WaitForSeconds(DirectionCooldown);
                RandomDirection();
                yield return new WaitForSeconds(changeDirectionInterval);
            }
            else
            {
                RandomDirection();
                yield return new WaitForSeconds(changeDirectionInterval);
            }
        }
    }

    public void RandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void NullDirection()
    {
        randomDirection = Vector2.zero;
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
        enemy.RandomDirection();
    }

    public void Update()
    {
        // Din kode for IdleState opdatering, hvis nødvendigt
    }

    public void Exit()
    {
        // Din kode for IdleState afslutning, hvis nødvendigt
    }
}
