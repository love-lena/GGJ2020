using System.Collections;
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
    [SerializeField]
    private GameObject endBlood;
    private Vector3 endBloodPos;
    public TextMeshProUGUI scoreText;
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
            if(Input.GetButtonDown("Start"))
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
            //TODO: Work on end game screen
            if(healthManager.GetHealth() <= 0.0)
            {
                gameState = "failed";
                EndOfGameAnimation();
                scoreText.SetText("Score : " + gameObject.GetComponent<SurvivalTimer>().GetTimer());
                endScreen.SetActive(true);
            }
        }

        if (gameState.Equals("failed"))
        {
            if(Input.GetButtonDown("Restart"))
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

    private void EndOfGameAnimation()
    {
        endBlood.SetActive(true);
        LeanTween.move(endBlood, new Vector2(endBlood.transform.position.x, 0), 2f);
        Invoke("doTheThing",2f);
        /*please*/
    }
    private void doTheThing()
    {
        endScreen.SetActive(true);
    }
}
