using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    private EnemyMovementInterface enemyMovementController;
    private EnemyMover enemyMover;

    private float currSpeed = 0;
    private Vector2 currDir = Vector2.zero;

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

    // Update is called once per frame
    void Update()
    {
        currSpeed = enemyMovementController.getSpeed();
        currDir = enemyMovementController.getDir();
        enemyMover.SetMovement(currDir, currSpeed);
    }
}
