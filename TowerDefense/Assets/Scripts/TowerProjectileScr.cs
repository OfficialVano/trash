using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScr : MonoBehaviour
{
    private Transform _target;

    public TowerProjectile selfProjectile;
    public Tower selfTower;

    private GameControllerScr _gcs;

    private void Start()
    {
        _gcs = FindObjectOfType<GameControllerScr>();

        selfProjectile = _gcs.allTowersProjectiles[selfTower.type];
    }
    
    private void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        _target = enemy;
    }

    private void Move()
    {
        if (_target != null)
        {
            if (Vector2.Distance(transform.position, _target.position) < 0.2f)
            {
                Hit();
            }
            else
            {
                var dir = _target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectile.speed);
            }
        }
        else
            Destroy(gameObject);
    }

    private void Hit()
    {
        switch (selfTower.type)
        {
            case (int)TowerType.FIRST_TOWER:
                _target.GetComponent<EnemyScr>().StartSlow(3,1);
                _target.GetComponent<EnemyScr>().TakeDamage(selfProjectile.damage);
                break;
            case (int)TowerType.SECOND_TOWER:
                _target.GetComponent<EnemyScr>().AOEDamage(2, selfProjectile.damage);
                break;
        }
        
        Destroy(gameObject);
    }
}
