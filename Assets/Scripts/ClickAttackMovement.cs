using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttackMovement : MonoBehaviour
{
    bool canAttack;
    GameObject player;
    Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        player = this.gameObject;
        playerTrans = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        player.GetComponent<PlayerController>().stopMovement();
        Vector2 currentPos = (Vector2)playerTrans.position;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 movementVec = mousePos - currentPos;

        for (float ft = 0.0F; ft <= 1.0F; ft += 0.1f)
        {
            player.transform.position = currentPos + movementVec * ft;
            yield return new WaitForSeconds(.01F);
        }

        canAttack = true;
    }
}