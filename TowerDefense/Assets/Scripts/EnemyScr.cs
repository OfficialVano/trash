using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyScr : MonoBehaviour
{
    private List<GameObject> _wayPoints = new List<GameObject>();

    public Enemy selfEnemy;

    private int _wayIndex = 0;


    private void Start()
    {
        GetWayPoints();

        GetComponent<SpriteRenderer>().color = selfEnemy.color;
    }

    private void Update()
    {
        Move();
    }

    private void GetWayPoints()
    {
        _wayPoints = GameObject.Find("LvlGroup").GetComponent<LvlManagerScr>().wayPoints;
    }

    private void Move()
    {
        var currWayPoint = _wayPoints[_wayIndex].transform;

        var currWayPos = new Vector3(
            currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
            currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2, -0.1f);

        var dir = currWayPos - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * selfEnemy.speed);

        if (Vector3.Distance(transform.position, currWayPos) < 0.1f)
            if (_wayIndex < _wayPoints.Count - 1)
                _wayIndex++;
            else
                Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        selfEnemy.health -= damage;

        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (selfEnemy.health <= 0)
        {
            MoneyManager.instance.gameMoney += 10;
            Destroy(gameObject);
        }
    }

    public void StartSlow(float duration, float slowValue)
    {
        StopCoroutine("GetSlow");
        selfEnemy.speed = selfEnemy.startSpeed;
        StartCoroutine(GetSlow(duration, slowValue));
    }

    private IEnumerator GetSlow(float duration, float slowValue)
    {
        selfEnemy.speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selfEnemy.speed = selfEnemy.startSpeed;
    }

    public void AOEDamage(float range, float damage)
    {
        List<EnemyScr> enemies = new List<EnemyScr>();
        
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(Vector2.Distance(transform.position,enemy.transform.position)<=range)
                enemies.Add(enemy.GetComponent<EnemyScr>());
        }

        foreach (var es in enemies)
        {
            es.TakeDamage(damage);
        }
    }
}
