using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private float maxHealth = 20f;
	public float health = 20f;
	private float lose = 1f;

	public bool dead = false;
	public bool sucking = false;
	public EnemyHealth sucker = null;
	public SuckingEnemy suckingEnemy = null;

	// Start is called before the first frame update
	void Start()
	{
		health = maxHealth;
		dead = false;
		suckingEnemy = gameObject.GetComponent<SuckingEnemy>();
	}

	public void Restart() {
		Start();
	}

	// Update is called once per frame
	void Update()
	{
		if (! dead) {
			float delta = Time.deltaTime;
			health -= (lose * delta) - GetSuckedAmmount(delta);
			if (health < 0) {
				health = 0;
				dead = true;
			}
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
		Debug.Log("Start the succ");

		enemy.StartSucking();
		suckingEnemy.StartSucking();
		sucker = enemy;
	}

	public void StopSucking() {
		sucker.StopSucking();
		suckingEnemy.StopSucking();
		// Destroy(sucker);
		sucker = null;
	}

	private float GetSuckedAmmount(float delta) {
		float ammount = 0f;
		if (sucker != null) {
			ammount = sucker.Sucked(delta);
			if (!sucker.Suckable()) { StopSucking(); }
		}
		return ammount;
	}
}
