using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    bool GameStart = false;
    public GameObject enemy;
    public Transform Player;
    public Transform SpawnPoint;
    public float placeHolder = 5f;
    private int enemyCount = 0;

    void Start()
    {
        GameStart = true;
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        while (GameStart && enemyCount < 3)
        {
            Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
            enemyCount++;
            yield return new WaitForSeconds(placeHolder);
        }
    }
}
