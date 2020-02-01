using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckingEnemy : MonoBehaviour
{
    public HealthManager healthManager;
    private bool isSucking;

    void Start()
    {
        healthManager = GameObject.FindWithTag("GameManager")
            .GetComponent<HealthManager>();
        isSucking = false;
    }

    void Update() { }

    //Goes on player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isSucking && collider.gameObject.tag.Equals("enemy"))
        {
            isSucking = true;
            healthManager.StartSucking(collider.gameObject.GetComponent<EnemyHealth>());
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (isSucking && collider.gameObject.tag.Equals("enemy"))
        {
            isSucking = false;
            healthManager.StopSucking();
        }
    }
}
