using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    private StateChangeManager manager;
    private string lastScene;
    private bool runTimer;
    private var Timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0.0;
        manager = this.gameObject.GetComponent<StateChangeManager>();
    }
    //
    // Update is called once per frame
    void Update()
    {
        lastScene = manager.GetState();
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
            Timer = 0.0;
            runTimer = true;
        }

        if (runTimer)
        {
            Timer += Time.deltaTime;
        }


    }
}
