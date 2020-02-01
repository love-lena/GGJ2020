using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    private EnemyMovementInterface enemyMovementController;
    private EnemyMover enemyMover;
    public bool scared = false;

    private float currSpeed = 0;
    private Vector2 currDir = Vector2.zero;

    [SerializeField]
    //determines how far away the enemy gets scared from
    private float eyesight;

    //putting this here, we could totally  refactor it to
    //be in a static class
    public enum EnemyState
    {
        chasing, notAggressive, gettingSucked, speaking
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyMovementController = GetComponent<EnemyMovementInterface>();
        if(enemyMovementController == null)
        {
            print("didn't find an enemy movement controller on gameobject " + gameObject.name);
        }
        enemyMover = GetComponent<EnemyMover>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(Vector2.Distance(transform.position, other.transform.position) > eyesight)
            {
                scared = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyMovementController.setScared(scared);
        currSpeed = enemyMovementController.getSpeed();
        currDir = enemyMovementController.getDir();
        enemyMover.SetMovement(currDir, currSpeed);
        if (scared)
        {
            scared = false;
        }
    }
}
