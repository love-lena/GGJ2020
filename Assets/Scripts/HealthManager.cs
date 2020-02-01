using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	float maxHealth;
	float health;
	float lose;

	// Start is called before the first frame update
	void Start()
	{
		maxHealth = health = 20f;
		lose = 1f;
	}

	// Update is called once per frame
	void Update()
	{
		health -= lose * Time.deltaTime;
	}
	
	public float GetHealth() {
		return health;
	}
	
	public float GetMaxHealth() {
		return maxHealth;
	}
}
