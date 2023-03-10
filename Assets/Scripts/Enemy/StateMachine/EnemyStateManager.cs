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

    //Public Variables ========================================================================
    public float searchRadius = 5f;
    public bool isPlayerHidden = false; 
    public bool isDetective = false;
    public List<Transform> patrolPoints = new List<Transform>();
    public NavMeshAgent agent;
    public Transform target;

    //Private Variables ========================================================================
    BoxCollider sight;
	SpriteRenderer rend;
	Animator anim;
	Vector3 dir;

	void Start()
    {
        GameEventSys.current.onPlayerHides += PlayerHidden;

        //Get Variables
        sight = this.GetComponent<BoxCollider>();

		//Get SpriteRenderer from child
		rend = transform.GetChild(0).GetComponent<SpriteRenderer>();

		//Get agent
		agent = this.GetComponent<NavMeshAgent>();

		//Get Animator
		anim = this.GetComponent<Animator>();

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

    void OnDisable()
    {
        GameEventSys.current.onPlayerHides -= PlayerHidden;
    }

    void OnDestroy()
    {
        GameEventSys.current.onPlayerHides -= PlayerHidden;
    }

    void Update()
    {
		// dir = Vector3.Project(agent.desiredVelocity.normalized, transform.right);
		dir = agent.desiredVelocity.normalized;
		if(dir.x > 0)
			rend.flipX = true;
        else
			rend.flipX = false;

		currentState.UpdateState(this); //Make specific enemy update state each frame
        // if(positionQ.Count > 0)
        // {
        //     Debug.Log(positionQ.Peek());
        //     agent.destination = positionQ.Dequeue();
        // }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        //If enemy sees player, chase
        if(col.gameObject.CompareTag("Player") && !isPlayerHidden) //if what enters the collider is the player AND the player is not hidden
        {
            SawPlayer(col.gameObject.transform);
        }
        if(col.gameObject.CompareTag("Evidence") && isDetective) //if the detective sees evidence
        {
            //Call FoundEvidence
            GameEventSys.current.FoundEvidence();

            //Destroy evidence ("Collect Evidence")
            Destroy(col.gameObject);
        }
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

    /* PlayerHidden ====================================
    *   - sets isPlayerHidden to "state"
    *===========================================*/
    public void PlayerHidden(bool state)
    {
        isPlayerHidden = state;
    }

    /* SawPlayer ====================================
    *   - Called when enemy sees player (i.e. not blocked or in a hiding spot)
    *   - Sets target to player, switches to chase state
    *===========================================*/
    public void SawPlayer(Transform player)
    {
            target = player;
            SwitchState(ChaseState);
    }

    /* SwitchState ====================================
    *   - Takes a vector 3 "spot" and finds the nearest spot on the navmesh
    *   - Returns a vector3
    *===========================================*/
    public Vector3 NearestOnNavmesh(Vector3 spot)
    {
        NavMeshHit nearestSpot;
        NavMesh.SamplePosition(spot, out nearestSpot, searchRadius, 1);
        return nearestSpot.position;
    }


    public IEnumerator Delay(float timePassed)
    {
        yield return new WaitForSeconds(timePassed);
        StopCoroutine(Delay(timePassed));
    }
}
