﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateChangeManager : MonoBehaviour
{
    private HealthManager healthManager;
    private string gameState;
    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private GameObject endScreen;
    private GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = this.gameObject.GetComponent<HealthManager>();
        gameState = "starting";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.Equals("starting"))
        {
            if(Input.GetKeyDown("space"))
            {
                healthManager.Restart();
                gameState = "playing";
                /*When the user places space the start screen is disabled*/
                startScreen.SetActive(false);

                /*spawn enemies at a location decided by the placement of the enemies gameObject*/
                StartGame();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        if (gameState.Equals("playing"))
        {
            if(healthManager.GetHealth() <= 0.0)
            {
                gameState = "failed";
                endScreen.SetActive(true);
            }
        }

        if (gameState.Equals("failed"))
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<EnemySpawner>().CleanUp();
                endScreen.SetActive(false);
                StartGame();
            }
        }
    }

    public string GetState()
    {
        return gameState;
    }

    private void StartGame()
    {
        GetComponent<EnemySpawner>().Spawn();
        gameState = "playing";
        healthManager.health = healthManager.GetMaxHealth();
    }
}
