using UnityEngine;

public class EnemyStateChase : EnemyBaseState
{
    /* Notes ====================================
    * EnemyStateChase
    *   - How did we get here   : Enemy spotted the player
    *   - What is happening     : The enemy is chasing the player
    *   - What will stop this   : The enemy can no longer see the player
    *===========================================*/

    RaycastHit sightHit;
    Vector3 lastSeenPos;
    int obstacleLayer =  1 << 6;
    bool canSeePlayer = true; //canSeePlayer must be separate from isPlayerHidden, because then chase state will never end

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public override void EnterState(EnemyStateManager enemy)
    {

    }

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public override void UpdateState(EnemyStateManager enemy)
    {
        if(canSeePlayer) //if player is visible
        {
            enemy.agent.destination = enemy.target.position;
            Debug.DrawLine(enemy.transform.position, enemy.target.position, Color.yellow);
            if(Physics.Linecast(enemy.transform.position, enemy.target.position, out sightHit, obstacleLayer)) //If can't see the player
            {
                canSeePlayer = false; //then the player is hidden
                // enemy.positionQ.Enqueue(sightHit.point);
                lastSeenPos = enemy.NearestOnNavmesh(sightHit.point);
                enemy.agent.destination = lastSeenPos;
            }
        }
        else
        {
            if(Vector3.Distance(enemy.agent.transform.position, lastSeenPos) - 1f < 0.1f)
            {
                
                enemy.SwitchState(enemy.SearchingState);
            }
            else
            {
                Debug.Log(Vector3.Distance(enemy.agent.transform.position, lastSeenPos));
                enemy.agent.destination = lastSeenPos;
            }
        }

    }
}
