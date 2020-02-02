using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemies;
    private GameObject enemiesSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        enemiesSpawned = Instantiate(enemies, enemies.transform.position, enemies.transform.rotation);
    }

    public void CleanUp()
    {
        Destroy(enemiesSpawned);
    }
}
