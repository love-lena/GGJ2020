using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFacingDirection : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector3 velocity;
    private EnemyStateController stateControl;
    private float angle;
    private Vector3 playerDir;
    private Transform player;
    private float scaredMult;
    [SerializeField]
    private float minMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbody = GetComponent<Rigidbody2D>();
        stateControl = GetComponent<EnemyStateController>();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (stateControl.currDir.magnitude > 0.1f)
        //{
        scaredMult = stateControl.scared ? -1 : 1;
        playerDir = scaredMult * ((Vector2)player.position - (Vector2)transform.position).normalized;
        if(playerDir.magnitude > minMagnitude)
        {
            angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
    }
}
