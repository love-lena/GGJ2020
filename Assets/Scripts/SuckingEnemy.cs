using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuckingEnemy : MonoBehaviour
{
    private ClickAttackMovement clickAttackMovementScript;
    public HealthManager healthManager;
    private bool isSucking;

    void Start()
    {
        healthManager = GameObject.FindWithTag("GameManager")
            .GetComponent<HealthManager>();
        isSucking = false;
        clickAttackMovementScript = gameObject.GetComponent<ClickAttackMovement>();
    }

    void Update() { }

    public void StartSucking() {
        isSucking = true;
    }

    public void StopSucking() {
        healthManager.StopSucking();
        isSucking = false;
    }

    //Goes on player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (clickAttackMovementScript.attacking && collider.gameObject.tag.Equals("Enemy") && collider.gameObject.GetComponent<EnemyStateController>().myState != EnemyStateController.EnemyState.dead)
        {
            //gameObject.GetComponent<ClickAttackMovement>().StopAttack();
            clickAttackMovementScript.attacking = false;
            clickAttackMovementScript.attackTimer = 0;
            clickAttackMovementScript.StartCoroutine("Cooldown");
            healthManager.StartSucking(collider.gameObject.GetComponent<EnemyHealth>());
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (isSucking && collider.gameObject.tag.Equals("Enemy"))
        {
            StopSucking();
            healthManager.StopSucking();
        }
    }

    public bool GetIsSucking() { return isSucking; }
}
