using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack()
    {
        Debug.Log(playerAnimator.GetBool("Attacking"));
        playerAnimator.SetBool("Attacking", true);
        Debug.Log(playerAnimator.GetBool("Attacking"));
    }

    public void EndAttack()
    {
        playerAnimator.SetBool("Attacking", false);
    }
}
