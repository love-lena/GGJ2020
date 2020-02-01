using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour, EnemyMovementInterface
{

    private GameObject player;
    private Vector2 target;

    [SerializeField]
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            print("player not found by enemy " + gameObject.name);
        }
    }

    public Vector2 getDir()
    {
        return (target - ((Vector2) transform.position)).normalized;
    }

    public float getSpeed()
    {
        return speed;
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
    }
}
