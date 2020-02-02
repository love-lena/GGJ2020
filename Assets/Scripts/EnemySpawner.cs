using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnSpread = 10;
    System.Random rnd = new System.Random();
    Transform spawners;

    private int numOfSpawners;

    [SerializeField]
    private int numberOfEnemies = 50;
    public GameObject enemyPrefab;
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        spawners = gameObject.transform.GetChild(0);
        numOfSpawners =  spawners.childCount;
        for (int i = 0; i < numberOfEnemies; i++) {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy() {
        Transform loc = spawners.GetChild(rnd.Next(0, numOfSpawners));
        Instantiate(enemyPrefab, loc.position + PositionOffset(), 
            loc.rotation, enemies.transform);
    }

    private Vector3 PositionOffset() {
        return new Vector3(rnd.Next(0,spawnSpread) * (float)rnd.NextDouble(), 
            rnd.Next(0,spawnSpread) * (float)rnd.NextDouble(), 
            1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

    }

    public void CleanUp()
    {
        Destroy(enemiesSpawned);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
            GameObject.Destroy(enemy);
    }
}
