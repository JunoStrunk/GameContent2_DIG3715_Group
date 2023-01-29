using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
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
