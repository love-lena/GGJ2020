using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private float maxHealth = 10f;
	public float health = 20f;
	private float lose = 1f;

	public bool dead = false;
	public bool sucking = false;
	public EnemyHealth sucker = null;
	public SuckingEnemy suckingEnemy = null;
	private PlayerMovement playerMovement;

    [SerializeField]
    private float hurtDamage = 3f;
	public bool debugging = false;
	public GameObject enemyPrefab;
	private AudioSource myAudio;

	private System.Random rand = new System.Random();
    public AudioClip hurt0;
    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip hurt3;
	public AudioClip succ;
	public AudioClip deathSound;
	private bool notPlayedDead = true;


	// Start is called before the first frame update
	void Start()
	{
		myAudio = GetComponent<AudioSource>();
		//Restart();
	}


	public void Restart() {
		playerMovement = GameObject.Find("NewPlayer").GetComponent<PlayerMovement>();
		health = maxHealth;
		dead = false;
		notPlayedDead = true;
		suckingEnemy = gameObject.GetComponent<SuckingEnemy>();
	}

	// Update is called once per frame
	void Update()
	{
		if(debugging) {
			health = 10f;
		}
		if (! dead) {
			float delta = Time.deltaTime;
			health -= (lose * delta) - GetSuckedAmmount(delta);
			if (health < 0) {
				health = 0;
				dead = true;
			}
		}
		if(health > maxHealth) {
			health = maxHealth;
		}
	}
	
	public float GetHealth() {
		return health;
	}
	
	public float GetMaxHealth() {
		return maxHealth;
	}

	public bool PlayerDead() {
		return dead;
	}

	public void StartSucking(EnemyHealth enemy) {
		myAudio.clip = succ;
        myAudio.Play();
		Debug.Log("Start the succ");
		playerMovement.takingInput = false;
		enemy.StartSucking();
		//suckingEnemy.StartSucking();
		sucker = enemy;
	}

	public void StopSucking() {
		if(sucker != null) {
			myAudio.Stop();
			sucker.StopSucking();
			//suckingEnemy.StopSucking();
			Instantiate(enemyPrefab, Random.insideUnitCircle * 50, Quaternion.identity);
			Destroy(sucker.gameObject);
			playerMovement.takingInput = true;
			sucker = null;
		}
	}

	private float GetSuckedAmmount(float delta) {
		float ammount = 0f;
		if (sucker != null) {
			ammount = sucker.Sucked(delta);
			if (!sucker.Suckable()) { StopSucking(); }
		}
		return ammount;
	}
    public void GetHit(float hurtDamage)
    {
		if (!dead)
        {
            switch (rand.Next(0, 4))
            {
                case 0:
                    myAudio.clip = hurt0;
                    break;
                case 1:
                    myAudio.clip = hurt1;
                    break;
                case 2:
                    myAudio.clip = hurt2;
                    break;
                case 3:
                    myAudio.clip = hurt3;
                    break;
            }
			myAudio.Play();
        } 
        health -= hurtDamage;
    }

	public bool IsSucking() {
		return sucker != null;
	}


}
