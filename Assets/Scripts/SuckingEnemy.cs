using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuckingEnemy : MonoBehaviour
{
    private ClickAttackMovement clickAttackMovementScript;
    public HealthManager healthManager;
    private bool isSucking;
    private float knockBackAmount = 1500f;

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
        Debug.Log("Triggerd: " + collider.gameObject.tag);
        if (clickAttackMovementScript.attacking && 
            collider.gameObject.tag.Equals("Enemy") && 
            collider.gameObject.GetComponent<EnemyStateController>().myState != EnemyStateController.EnemyState.dead)
        {
            //gameObject.GetComponent<ClickAttackMovement>().StopAttack();
            clickAttackMovementScript.attacking = false;
            clickAttackMovementScript.attackTimer = 0;
            clickAttackMovementScript.StartCoroutine("Cooldown");
            healthManager.StartSucking(collider.gameObject.GetComponent<EnemyHealth>());
        } 
        else if (collider.gameObject.tag.Equals("Weapon")) 
        {
            Debug.Log("Weapon");
            GameObject parent = collider.gameObject.transform.parent.gameObject;
            if (parent.GetComponent<EnemyStateController>().myState == 
                EnemyStateController.EnemyState.attacking)
            {
                float damage = parent.GetComponent<EnemyHealth>().DoDamage();
                healthManager.GetHit(damage);
                PlayerKnockback(collider.gameObject.transform);
            }
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

    private void PlayerKnockback(Transform source) {
        Debug.Log("Knockback");
        if(isSucking) {
            //Stop sucking
            StopSucking();
        } 
        //Disable movement
        StartCoroutine("DisableMovementKnockback");
        //Push Player
        Vector2 pushForce = transform.position - source.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2();
        gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce.normalized * knockBackAmount);
    }

    private IEnumerator DisableMovementKnockback() {

        gameObject.GetComponent<PlayerMovement>().takingInput = false;
        gameObject.GetComponent<ClickAttackMovement>().readyToAttack = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<PlayerMovement>().takingInput = true;
        gameObject.GetComponent<ClickAttackMovement>().readyToAttack = true;
        yield return null;
    }
}
