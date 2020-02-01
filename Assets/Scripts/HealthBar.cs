using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public float health;
	public float maxHealth;
	GameObject gameManager;
	HealthManager healthManager;
	GameObject mask;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.FindWithTag("GameManager");
		mask = gameObject.transform.Find("HealthBarMask").gameObject;
		healthManager = gameManager.GetComponent<HealthManager>();
		health = healthManager.GetHealth();
		maxHealth = healthManager.GetMaxHealth();
	}

	// Update is called once per frame
	void Update()
	{
		health = healthManager.GetHealth();
		mask.transform.localScale = new Vector3(health/maxHealth, 1, 1);
	}
}
