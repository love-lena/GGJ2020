using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttackMovement : MonoBehaviour
{
    bool canAttack; 
    GameObject player;
    Transform playerTrans;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        player = this.gameObject;
        playerTrans = player.GetComponent<Transform>();
        speed = 50.0F;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            Debug.Log("Attack!");
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
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

        canAttack = true;
        yield return null;
    }
}
