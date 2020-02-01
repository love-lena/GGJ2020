using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour, EnemyMovementInterface
{

    private GameObject player;
    private Vector2 target;

    [SerializeField]
    private float speed;

    private int scaredMult;


    // Start is called before the first frame update
    void Start()
    {
        //1 for not scared, -1 for scared
        scaredMult = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            print("player not found by enemy " + gameObject.name);
        }
    }

    public Vector2 getDir()
    {
        return scaredMult * (target - ((Vector2) transform.position)).normalized;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void setScared(bool scared)
    {
        scaredMult = scared ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
    }
}
