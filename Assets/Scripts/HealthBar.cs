using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	float health;
	float maxHealth;
	GameObject gm;
	HealthManager hs;
	GameObject mask;

	// Start is called before the first frame update
	void Start()
	{
		gm = GameObject.FindWithTag("GameManager");
		mask = this.gameObject.transform.Find("HealthBarMask").gameObject;
		hs = gm.GetComponent<HealthManager>();
		health = hs.GetHealth();
		maxHealth = hs.GetMaxHealth();
	}

	// Update is called once per frame
	void Update()
	{
		health = hs.GetHealth();
		mask.transform.localScale = new Vector3(health/maxHealth, 1, 1);
	}
}
