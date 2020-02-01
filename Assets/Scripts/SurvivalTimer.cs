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
    }
    
    // Update is called once per frame
    void Update()
    {
        lastState = manager.GetState();
        if(lastState.Equals("starting") && manager.GetState().Equals("playing"))
        {
            runTimer = true;
        }

        if (lastState.Equals("playing") && manager.GetState().Equals("failed"))
        {
            runTimer = false;
        }

        if (lastState.Equals("failed") && manager.GetState().Equals("playing"))
        {
            timer = 0.0f;
            runTimer = true;
        }

        if (runTimer)
        {
            timer += Time.deltaTime;
        }


    }
}
