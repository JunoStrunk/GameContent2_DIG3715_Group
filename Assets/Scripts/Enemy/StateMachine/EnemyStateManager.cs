using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    /* Notes ====================================
    * EnemyStateManager
    *   - Controls switching states
    *   - Hold references to all of the different states
    *===========================================*/

    //Current State
    EnemyBaseState currentState;

    //All states
    public EnemyStatePatrol PatrolState = new EnemyStatePatrol();
    public EnemyStateChase ChaseState = new EnemyStateChase();
    public EnemyStateSearching SearchingState = new EnemyStateSearching();
    public EnemyStateIncapacitated IncapacitatedState = new EnemyStateIncapacitated();

    //Public Variables
    public List<Transform> patrolPoints = new List<Transform>();
    public NavMeshAgent agent;

    void Start()
    {
        //Get agent
        agent = this.GetComponent<NavMeshAgent>();
        
        //Get patrol points
        Transform pathsParent = this.transform.parent.GetChild(this.transform.GetSiblingIndex()+1); //gets Paths gameobject
        for(int childIter = 0; childIter < pathsParent.childCount; childIter++) //Loop through paths children
        {
            patrolPoints.Add(pathsParent.GetChild(childIter)); //Add patrol points (Children) to list
        }

        //Set State and enter state
        currentState = PatrolState; //Set current state to patrol
        currentState.EnterState(this); //Make specific enemy enter the current state
    }

    void Update()
    {
        currentState.UpdateState(this); //Make specific enemy update state each frame
    }
    
    /* SwitchState ====================================
    *   - Switches state to whatever state is passed in
    *   - Called in Update state of the state's script
    *===========================================*/
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state; //update current state to whatever the next state is
        currentState.EnterState(this); //Set state for gameobject
    }
}
