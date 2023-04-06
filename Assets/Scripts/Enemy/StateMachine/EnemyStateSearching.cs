using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSearching : EnemyBaseState
{
	/* Notes ====================================
    * EnemyStateSearching
    *   - How did we get here   : Enemy was chasing the player but lost sight of them
    *   - What is happening     : Enemy is going towards the last place they saw the player
    *   - What will stop this   : x amount of time passes without spotting player
    *===========================================*/

	Vector3 randDirection;
	Vector3 searchPostion;
	bool needSearchPos;
	Queue<Vector3> searchTransforms = new Queue<Vector3>();
	int searchIter;
	int searchIterBound = 4;

	/* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
	public override void EnterState(EnemyStateManager enemy)
	{
		// Debug.Log("Searching");
		// for(searchIter = 0; searchIter < searchIterBound; searchIter++)
		// {
		//     randDirection = Random.insideUnitSphere * enemy.searchRadius; //pick a random spot in range

		//     randDirection += enemy.agent.transform.position;
		//     goToPosition = enemy.NearestOnNavmesh(randDirection);

		//     searchTransforms.Enqueue(goToPosition);
		// }
		// Debug.Log("Enter Search");

		needSearchPos = true;
		searchIter = 0;
	}

	/* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
	public override void UpdateState(EnemyStateManager enemy)
	{
		if (searchIter < searchIterBound)
		{
			//If enemy does not have a random search spot, generate one
			//If enemy has a random search spot and is at it, get another one, increase iter
			//If enemy has a random search spot and is not at it, go to it

			if (needSearchPos)
			{
				searchPostion = generateSearchSpot(enemy);
				needSearchPos = false;
			}
			else
			{
				// Debug.DrawLine(enemy.transform.position, searchPostion, Color.red);
				if (Vector3.Distance(enemy.agent.transform.position, searchPostion) - 2f < 0.1f) //if enemy is at search spot
				{
					searchIter++;
					needSearchPos = true;
				}
				else
				{
					enemy.agent.destination = searchPostion; //go towards searchPosition
				}
			}
		}
		else
		{
			enemy.SwitchState(enemy.PatrolState);
		}


		// if(searchTransforms.Count > 0)
		// {
		//     enemy.agent.destination = searchTransforms.Peek();

		//     if(Vector3.Distance(enemy.agent.transform.position, searchTransforms.Peek()) == 1)
		//     {
		//         //Select next target point
		//         searchTransforms.Dequeue();

		//         enemy.transform.LookAt(searchTransforms.Peek());
		//     }
		// }
		// else
		// {
		//     enemy.SwitchState(enemy.PatrolState);
		// }
	}

	Vector3 generateSearchSpot(EnemyStateManager enemy)
	{
		randDirection = Random.insideUnitSphere * enemy.searchRadius; //pick a random spot in range
		randDirection += enemy.agent.transform.position;
		return enemy.NearestOnNavmesh(randDirection);
	}

}
