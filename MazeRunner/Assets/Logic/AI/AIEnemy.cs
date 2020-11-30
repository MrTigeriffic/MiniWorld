using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIEnemy : MonoBehaviour {

	public enum ENEMY_STATE {PATROL, CHASE, ATTACK };
	

	public ENEMY_STATE CurrentState//accessor method to change state in the state machine
	{
		get { return currentState; }
		set
		{
			currentState = value;
			StopAllCoroutines();

			switch (currentState)
			{
				case ENEMY_STATE.PATROL:
                    aiText.text = "AI: " + currentState.ToString();
                    //Debug.Log(currentState);
                    StartCoroutine(AIPatrol());
				break;
				case ENEMY_STATE.CHASE:
                    aiText.text = "AI: " + currentState.ToString();
                    StartCoroutine(AIChase());
				break;
				case ENEMY_STATE.ATTACK:
                    aiText.text = "AI: " + currentState.ToString();
                    StartCoroutine(AIAttack());
				break;
			}
		}
	}

    [SerializeField]
    private ENEMY_STATE currentState = ENEMY_STATE.PATROL;//setting up AI states and what will happen in each state
    private LineOfSight ThisLineOfSight = null;//can npc see the player so 
    private NavMeshAgent ThisAgent = null;
    private Transform ThisTransform = null;//npc transform

    public HealthPoints PlayerHealth = null;
    public Transform PlayerTransform = null;
    public Transform PatrolDest = null;//get reference to Destination object

    public Text aiText = null;
    public float MaxDamage = 10f;

    //these co routines will have looping behaviors
    public IEnumerator AIPatrol()
	{
		while(currentState == ENEMY_STATE.PATROL)
		{
            ThisLineOfSight.Sensitiv = LineOfSight.SightSensitivity.SRTICT;
            ThisAgent.Resume();
            ThisAgent.SetDestination(PatrolDest.position);//set the position
            //while the destination object next position is set 
            while (ThisAgent.pathPending)
            {
                yield return null;
            }
            if (ThisLineOfSight.CanSeeTarget)
            {
                ThisAgent.Stop();
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }
			yield return null;//stop the loop 
		}
		yield break;
	}
	public IEnumerator AIChase()
	{
		while (currentState == ENEMY_STATE.CHASE)
		{
            ThisLineOfSight.Sensitiv = LineOfSight.SightSensitivity.LOOSE;
            //chase to last known position
            ThisAgent.Resume();
            ThisAgent.SetDestination(ThisLineOfSight.LastknownSighting); //NPC will go to last known position of the player and not the player itself
            while(ThisAgent.pathPending)
			    yield return null;

            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
            {
                ThisAgent.Stop();
                if (!ThisLineOfSight.CanSeeTarget)
                    CurrentState = ENEMY_STATE.PATROL;
                else//reached destination and can see player, switch to attack
                    CurrentState = ENEMY_STATE.ATTACK;

                yield break;
            }
            //wait until next frame
            yield return null;
        }
        
	}
	public IEnumerator AIAttack()
	{
		while (currentState == ENEMY_STATE.ATTACK)
		{
            ThisAgent.Resume();
            ThisAgent.SetDestination(PlayerTransform.position);
            while(ThisAgent.pathPending)
			    yield return null;
            //has player run away?
            if(ThisAgent.remainingDistance > ThisAgent.stoppingDistance)
            {
                //Change to chase
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }
            else
            {
                //attack
                PlayerHealth.HealPoints -= MaxDamage * Time.deltaTime;
                
            }
            yield return null;
		}
		yield break;
	}

	// Use this for initialization
	void Awake () {
		ThisAgent = GetComponent<NavMeshAgent>();
        ThisLineOfSight = GetComponent<LineOfSight>();
        ThisTransform = GetComponent<Transform>();
        PlayerTransform = PlayerHealth.GetComponent<Transform>();
        Text aiText = GetComponent<Text>();
	}

    private void Start()
    {
        CurrentState = ENEMY_STATE.PATROL;
    }

    // Update is called once per frame
    void Update () {
		ThisAgent.SetDestination(PatrolDest.position);
	}
}
