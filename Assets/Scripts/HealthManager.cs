using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private float maxHealth = 20f;
	public float health = 20f;
	private float lose = 1f;

	public bool dead = false;

    [SerializeField]
    private float hurtDamage = 3f;

	// Start is called before the first frame update
	void Start()
	{
		health = maxHealth;
		dead = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (! dead) {
			health -= lose * Time.deltaTime;
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

    public void GetHit()
    {
        health -= hurtDamage;
    }


}
