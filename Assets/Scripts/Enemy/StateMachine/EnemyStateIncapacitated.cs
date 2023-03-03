using UnityEngine;

public class EnemyStateIncapacitated : EnemyBaseState
{
    /* Notes ====================================
    * EnemyStateIncapacitated
    *   - How did we get here   : The player incapacitated the enemy (passed out or dead?)
    *   - What is happening     : The enemy is not moving or looking
    *   - What will stop this   : Passed out: Time passes, Dead: Nothing end of line.
    *===========================================*/

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public override void EnterState(EnemyStateManager enemy)
    {
        // Debug.Log("Incapacitated");
    }

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
