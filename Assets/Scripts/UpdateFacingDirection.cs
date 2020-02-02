using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFacingDirection : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector3 velocity;
    private EnemyStateController stateControl;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        stateControl = GetComponent<EnemyStateController>();
            
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stateControl.currDir.magnitude > 0.1f)
        {
            angle = Mathf.Atan2(stateControl.currDir.y, stateControl.currDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
