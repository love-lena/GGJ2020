using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckingEnemy : MonoBehaviour
{
    public HealthManager healthManager;
    private bool isSucking;

    void Start()
    {
        isSucking = false;
    }

    void Update() { 
}

    //Goes on player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("enemy"))
        {
            isSucking = true;
            healthManager.Sucking(collider.gameObjects);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("enemy"))
        {
            isSucking = false;
        }
    }
}
