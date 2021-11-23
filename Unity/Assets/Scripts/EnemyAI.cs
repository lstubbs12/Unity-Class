using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAI : MonoBehaviour
{
    public enum State { Wander, Chase, Shoot};

    [SerializeField] Transform player;
    [SerializeField] private State state;
    private NavMeshAgent ai;

    [Header("Wander Settings")]
    public float circleDist;
    public float circleRadius;
    public float circleAnghle;
    public Vector2 variance;

    private Dictionary<State, System.Action> enter;
    private Dictionary<State, System.Action> exit;
    private Dictionary<State, System.Action> execute;



    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        enter = new Dictionary<State, System.Action>() {
            {State.Wander, WanderEnter },
            {State.Chase, ChaseEnter },
            {State.Shoot, ShootEnter }
        };
        exit = new Dictionary<State, System.Action>() {
            {State.Wander, WanderExit},
            {State.Chase, ChaseExit},
            {State.Shoot, ShootExit}
        };
        execute = new Dictionary<State, System.Action>() {
            {State.Wander, WanderExecute},
            {State.Chase, ChaseExecute},
            {State.Shoot, ShootExecute}
        };

        Transition(State.Wander);
    }

    private void Update()
    {
        ai.destination = player.position;
        execute[state]();
        
    }


    void Transition(State nextState)
    {
        exit[state]();
        state = nextState;
        enter[state]();
    }

    void WanderEnter()
    {

    }

    void WanderExit()
    {

    }

    void WanderExecute()
    {

    }

    void ChaseEnter()
    {

    }

    void ChaseExit()
    {

    }

    void ChaseExecute()
    {

    }

    void ShootEnter()
    {

    }

    void ShootExit()
    {

    }

    void ShootExecute()
    {

    }
}
