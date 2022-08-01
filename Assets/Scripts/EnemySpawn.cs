using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int enemyCount = 0;
    public GameObject[] spawnPoints;
    public GameObject objectToSpawn;
    public bool canSpawn = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            canSpawn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            canSpawn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && enemyCount < 1)
        {
            int picker = (int) Random.Range(0, 4);
            Instantiate(objectToSpawn, spawnPoints[picker].transform.position, Quaternion.identity);
            enemyCount++;
        }
    }
}
