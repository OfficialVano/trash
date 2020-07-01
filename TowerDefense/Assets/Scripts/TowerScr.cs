using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerScr : MonoBehaviour
{
    public GameObject projectile;

    private Tower _selfTower;
    public TowerType selfType;
    
    private GameControllerScr _gcs;

    private void Start()
    {
        _gcs = FindObjectOfType<GameControllerScr>();

        _selfTower = _gcs.allTowers[(int) selfType];
        //GetComponent<SpriteRenderer>().sprite = _selfTower.spr;
        
        InvokeRepeating("SearchTarget",0,0.1f);
    }

    private void Update()
    {
        if (_selfTower.currCooldown > 0)
            _selfTower.currCooldown -= Time.deltaTime;
    }

    private bool CanShoot()
    {
        return _selfTower.currCooldown <= 0;
    }

    private void SearchTarget()
    {
        if (CanShoot())
        {

            Transform nearestEnemy = null;
            var nearestEnemyDistance = Mathf.Infinity;

            foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var currDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (currDistance < nearestEnemyDistance && currDistance <= _selfTower.range)
                {
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = currDistance;
                }

                if (nearestEnemy != null)
                    Shoot(nearestEnemy);
            }
        }
    }

    private void Shoot(Transform enemy)
    {
        _selfTower.currCooldown = _selfTower.cooldown;

        var proj = Instantiate(projectile);
        proj.GetComponent<TowerProjectileScr>().selfTower = _selfTower;
        
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileScr>().SetTarget(enemy);
    }
}
