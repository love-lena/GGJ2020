using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    private StateChangeManager manager;
    private string lastState;
    private bool runTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        manager = this.gameObject.GetComponent<StateChangeManager>();
        lastState = "starting";
    }
    
    // Update is called once per frame
    void Update()
    {
        if(lastState.Equals("starting") && manager.GetState().Equals("playing"))
        {
            runTimer = true;
            lastState = "playing";
        }

        else if (lastState.Equals("playing") && manager.GetState().Equals("failed"))
        {
            runTimer = false;
            lastState = "failed";
        }

        else if (lastState.Equals("failed") && manager.GetState().Equals("playing"))
        {
            timer = 0.0f;
            runTimer = true;
            lastState = "playing";
        }

        if (runTimer)
        {
            timer += Time.deltaTime;
        }


    }

    public float GetTimer() {
        return timer;
    }
}
