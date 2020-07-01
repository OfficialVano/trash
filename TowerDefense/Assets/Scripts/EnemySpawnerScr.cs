using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class EnemySpawnerScr : MonoBehaviour
{
    public LvlManagerScr LMS;
    private GameControllerScr _gcs;
    
    public GameObject enemyPref;
    
    public float timeToSpawn = 3;

    private int _spawnCount=0;

    private void Start()
    {
        _gcs = FindObjectOfType<GameControllerScr>();
    }

    private void Update()
    {
        if (timeToSpawn <= 0)
        {
            StartCoroutine(SpawnEnemy(_spawnCount + 1));
            timeToSpawn = 4;
        }

        timeToSpawn -= Time.deltaTime;
    }

    private IEnumerator SpawnEnemy(int enemyCount)
    {
        _spawnCount++;

        for (var i = 0; i < enemyCount; i++)
        {
            var tmpEnemy = Instantiate(enemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);

            tmpEnemy.GetComponent<EnemyScr>().selfEnemy = new Enemy(_gcs.allEnemies[new Random().Next(0, _gcs.allEnemies.Count)]);
            
            var startCellPos = LMS.wayPoints[0].transform;
            var startPos = new Vector3(startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                       startCellPos.position.y - startCellPos.GetComponent<SpriteRenderer>().bounds.size.y / 2, -0.1f);

            tmpEnemy.transform.position = startPos;
                
            yield return new WaitForSeconds(0.3f);
        }
    }
}
