using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttackMovement : MonoBehaviour
{
    bool canAttack; 
    GameObject player;
    Transform playerTrans;
    private float speed;
    private string gameState;
    private GameObject gameManager;
    public bool controllerInput = false;
    private Coroutine attackCorInstance;
    public bool attacking = false;
    
    private float timeToAttack = 0.1f;
    public float attackTimer = 0f;
    private float attackSpeed = 50f;

    public bool readyToAttack = true;
    
    private float timeToReadyAttack = 0.75f;
    public float attackRechargeTimer = 0f;
    private Quaternion lockRotation;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        player = this.gameObject;
        playerTrans = player.GetComponent<Transform>();
        speed = 50.0F;
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

        if(attacking) {
            if(attackTimer >= timeToAttack) {
                attacking = false;
                attackTimer = 0;
                readyToAttack = false;
                //StartCoroutine("Cooldown");
            } else {
                attackTimer += Time.deltaTime;
                transform.Translate(Vector3.up * Time.deltaTime * attackSpeed);
            }
            transform.rotation = lockRotation;
        }

        gameState = gameManager.GetComponent<StateChangeManager>().GetState();
        
        if ((Input.GetButtonDown("Attack") || Input.GetButtonDown("Attack2")) && readyToAttack && (gameState == "playing"))
        {
            canAttack = false;
            attacking = true;
            lockRotation = transform.rotation;
            //attackCorInstance = StartCoroutine("Attack");
            //StartCoroutine("Cooldown");
            //GameObject.FindWithTag("Player").GetComponent<SuckingEnemy>().StartSucking();
        } else if (Input.GetButtonUp("Attack") || Input.GetButtonUp("Attack2")) {
            attacking = false;
            GetComponent<SuckingEnemy>().StopSucking();
            //StopCoroutine("Attack");
        }

        if(readyToAttack == false) {
            if(attackRechargeTimer >= timeToReadyAttack) {
                attackRechargeTimer = 0f;
                readyToAttack = true;
            } else {
                attackRechargeTimer += Time.deltaTime;
            }
        }

        
    }

    public void StopAttack() { StopCoroutine(attackCorInstance); }

    //Not used
    IEnumerator Attack()
    {
        if(controllerInput) {
            canAttack = false;

            Vector3 currentPos = (Vector2)playerTrans.position;
            
            Vector3 targetForward = player.transform.rotation * Vector3.up;
            targetForward = targetForward.normalized * 8f;
            
            Vector3 targetPos = currentPos + targetForward;
            Vector3 movementVec = (targetPos - currentPos);
            movementVec.z = 0;

            for (float ft = 0.0F; ft <= 1.0F; ft += 0.1f)
            {
                player.transform.position = currentPos + movementVec * ft;
                yield return new WaitForSeconds(.01F);
            }
            //playerTrans.position += movementVec.normalized * Time.deltaTime * speed;
            yield return null;

        } else {
            canAttack = false;

            //player.GetComponent<PlayerController>().SetState("attacking");
            Vector3 currentPos = (Vector2)playerTrans.position;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 movementVec = (mousePos - currentPos);
            movementVec.z = 0;

            for (float ft = 0.0F; ft <= 1.0F; ft += 0.1f)
            {
                player.transform.position = currentPos + movementVec * ft;
                yield return new WaitForSeconds(.01F);
            }
            //playerTrans.position += movementVec.normalized * Time.deltaTime * speed;
            yield return null;
        }
    }

    //Not used
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.75f);
        canAttack = true;
    }
}
