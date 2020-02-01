using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeManager : MonoBehaviour
{
    private HealthManager healthManager;
    private string gameState;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = this.gameObject.GetComponent<HealthManager>();
        gameState = "starting";
    }

    // Update is called once per frame
    void Update()
    {
        while (gameState.Equals("starting"))
        {
            if(!Input.GetKeyDown(KeyCode.Escape) && Input.anyKey)
            {
                healthManager.Restart();
                gameState = "playing";
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        while (gameState.Equals("playing"))
        {
            if(healthManager.GetHealth() >= 0.0)
            {
                gameState = "failed";
            }
        }

        while (gameState.Equals("failed"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameState = "playing";
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = "starting";
            }
        }
    }

    public string GetState()
    {
        return gameState;
    }
}
