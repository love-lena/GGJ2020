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
        gameState = gameManager.GetComponent<StateChangeManager>().GetState();
        if (Input.GetButtonDown("Attack") && canAttack && (gameState == "playing"))
        {
            StartCoroutine("Attack");
            StartCoroutine("Cooldown");
        }
    }

    IEnumerator Attack()
    {
        if(controllerInput) {
            canAttack = false;

            Vector3 currentPos = (Vector2)playerTrans.position;
            
            Vector3 targetForward = player.transform.rotation * Vector3.up;
            targetForward = targetForward.normalized * 3f;
            
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

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
