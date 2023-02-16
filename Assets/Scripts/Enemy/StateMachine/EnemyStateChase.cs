using UnityEngine;

public class EnemyStateChase : EnemyBaseState
{
    /* Notes ====================================
    * EnemyStateChase
    *   - How did we get here   : Enemy spotted the player
    *   - What is happening     : The enemy is chasing the player
    *   - What will stop this   : The enemy can no longer see the player
    *===========================================*/

    //Public Variables
    Ray sightLine;
    RaycastHit sightHit;

    //Private Variables
    int obstacleLayer =  1 << 6;

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
        if(!enemy.isPlayerHidden) //if player is visible
        {
            enemy.agent.destination = enemy.target.position;
            Debug.DrawLine(enemy.transform.position, enemy.target.position, Color.yellow);
            if(Physics.Linecast(enemy.transform.position, enemy.target.position, out sightHit, obstacleLayer)) //If can't see the player
            {
                enemy.isPlayerHidden = true; //then the player is hidden
            }
        }
        else
        {
            enemy.agent.destination = sightHit.point; //go to last place seen
        }

    }
}
