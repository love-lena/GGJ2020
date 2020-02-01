using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    public Camera mainCam;
    private Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCam.transform.position = playerTrans.transform.position;
    }
}
