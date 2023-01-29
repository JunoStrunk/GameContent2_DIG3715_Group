using UnityEngine;

public class EnemyStateSearching : EnemyBaseState
{
    /* Notes ====================================
    * EnemyStateSearching
    *   - How did we get here   : Enemy was chasing the player but lost sight of them
    *   - What is happening     : Enemy is going towards the last place they saw the player
    *   - What will stop this   : x amount of time passes without spotting player
    *===========================================*/

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
        
    }
}
