using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    private EnemyMovementInterface enemyMovementController;
    private EnemyMover enemyMover;
    public bool scared = false;
    public bool stationary = false;
    private bool faceSeen;

    private GameObject player;
    private HealthManager healthManager;

    private float currSpeed = 0;
    public Vector2 currDir = Vector2.zero;

    [SerializeField]
    private float timeSpentAfraid = 2f;
    private float fearTimer;

    private float weaponRange;
    [SerializeField]
    private float attackMovSpeedMultiplier = 0.5f;

    [SerializeField]
    private float attackCooldownTime = 1f;
    private float attackCooldownTimer = 0f;

    private float attackTime = 0.5f;
    private EnemyWeapon weapon;
    private Animator enemyAnimator;

    //putting this here, we could totally  refactor it to
    //be in a static class
    public enum EnemyState
    {
        chasing, afraid, resting, attacking, gettingSucked, 
        speaking, notAggressive, dead
    }

    public EnemyState myState;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetComponentInChildren<EnemyWeapon>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthManager = GameObject.Find("GameManager").GetComponent<HealthManager>();
        weaponRange = weapon.weaponRange;
        attackTime = weapon.activeTime;
        myState = EnemyState.chasing;
        enemyMovementController = GetComponent<EnemyMovementInterface>();
        if(enemyMovementController == null)
        {
            // print("didn't find an enemy movement controller on gameobject " + gameObject.name);
        }
        enemyMover = GetComponent<EnemyMover>();
        enemyAnimator = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ScaryFace")
        {
            faceSeen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //sets stationary and scared on the movement controller,
        //and determines if a state change is necessary
        updateTimers();
        stateMachine();
        currSpeed = enemyMovementController.getSpeed();
        currDir = enemyMovementController.getDir();
        enemyMover.SetMovement(currDir, currSpeed);
    }
    private void updateTimers()
    {
        if (attackCooldownTimer > 0)
            attackCooldownTimer -= Time.deltaTime;
        if (fearTimer> 0)
            fearTimer -= Time.deltaTime;
    }
    private void stateMachine()
    {
        Debug.Log("Sucking: " + healthManager.IsSucking());
        switch (myState)
        {
            //IMPORTANT:
            //every state MUST set stationary and scared 
            case EnemyState.chasing:
                stationary = false;
                scared = false;
                if (faceSeen && !healthManager.IsSucking())
                {
                    fearTimer = timeSpentAfraid;
                    myState = EnemyState.afraid;
                    //important to remember to disable these transition variables
                    faceSeen = false;
                }
                if (Vector2.Distance(player.transform.position, transform.position) < weaponRange) 
                {
                    myState = EnemyState.attacking;
                }
                break;
            case EnemyState.afraid:
                stationary = false;
                scared = true;
                if(fearTimer <= 0 || healthManager.IsSucking())
                {
                    if (!faceSeen)
                    {
                        myState = EnemyState.chasing;
                    }
                    else
                    {
                        faceSeen = false;
                        fearTimer = timeSpentAfraid;
                    }
                }
                
                break;
            case EnemyState.resting:
                stationary = true;
                scared = false;
                //can set to this state from the speech controller
                // OR this happens every time they're done being afraid
                break;
            case EnemyState.attacking:
                stationary = true;
                scared = false;
                if(attackCooldownTimer <= 0)
                {
                    attackCooldownTimer = attackCooldownTime;
                    weapon.attack();
                    enemyAnimator.SetBool("Attacking", true);
                    Invoke("StopAttacking", .3f);
                }
                if (Vector2.Distance(player.transform.position, transform.position) > weaponRange)
                {
                    myState = EnemyState.chasing;
                }
                if (faceSeen)
                {
                    myState = EnemyState.afraid;
                }
                    
                //check distance to player, exit if further than attack range
                break;
            case EnemyState.gettingSucked:
                stationary = true;
                scared = false;
                //whatever state caused this transition (afraid, chasing, resting or non aggro)
                //will need to set a succ timer

                // this state queries it and waits until it runs out or the player stops
                // the succ to transition to the dead state

                //OR we could just have the player tell the enemy they're done succin
                break;
            case EnemyState.dead:
                stationary = true;
                scared = false;
                break;
            case EnemyState.speaking:
                stationary = false;
                scared = false;
                //transition: very dependent on entry reason. Should have a speach controller
                // script that will manually change this state from its context


                //we may not need this state at all if we dont want speech to affect enemy movement
                break;
            case EnemyState.notAggressive:
                stationary = true;
                scared = false;
                //transition: when player is within certain range, could transition to a speaking 
                // state and then to chasing, or go straight to chasing
                //TO IMPLEMENT: random movement for idling
                break;
            default:
                break;
        }
        enemyMovementController.setStationary(stationary);
        enemyMovementController.setScared(scared);
    }

    public void SetState(EnemyState newState) 
    {
        myState = newState;
    }
    private void StopAttacking()
    {
        enemyAnimator.SetBool("Attacking", false);
    }
}
