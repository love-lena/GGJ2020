using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    private Transform endAttack;
    private Quaternion attackStartRotation;
    private Vector3 attackStartPosition;
    private int wepRotLtId;
    private int wepPosLtId;
    public bool isAttacking;
    [SerializeField]
    private float rotTime;
    [SerializeField]
    private float movTime;
    [SerializeField]
    public float activeTime;

    [SerializeField]
    public float weaponRange;

    public Collider2D coll;
    private AudioSource weaponAudio;
    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GetComponent<AudioSource>();
        isAttacking = false;
        attackStartPosition = gameObject.transform.localPosition;
        attackStartRotation = gameObject.transform.localRotation;
        endAttack = transform.parent.GetChild(1);

        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wepPosLtId != 0 && wepRotLtId != 0)
            {
                if (LeanTween.isTweening(wepPosLtId))
                    LeanTween.cancel(wepPosLtId);
                if (LeanTween.isTweening(wepRotLtId))
                    LeanTween.cancel(wepRotLtId);
            }
            attack();
        }
    }
    public void stopAttacking()
    {
        coll.enabled = false;
        isAttacking = false;
        //if the tweens have been called at least once
        if(wepPosLtId != 0 && wepRotLtId != 0)
        {
            if (LeanTween.isTweening(wepPosLtId))
                LeanTween.cancel(wepPosLtId);
            if (LeanTween.isTweening(wepRotLtId))
                LeanTween.cancel(wepRotLtId);
        }
        transform.localPosition = attackStartPosition;
        transform.localRotation = attackStartRotation;
    }

    public void attack()
    {
        weaponAudio.Play();
        wepRotLtId = LeanTween.rotateLocal(gameObject, endAttack.localEulerAngles, rotTime).id;
        wepPosLtId = LeanTween.moveLocal(gameObject, endAttack.localPosition, movTime).id;
        StartCoroutine("weaponDisable");
        coll.enabled = true;
        isAttacking = true;
    }


    IEnumerator weaponDisable()
    {
        yield return new WaitForSeconds(activeTime);
        stopAttacking();

    }



}
