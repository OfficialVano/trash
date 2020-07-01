using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    public string name;
    
    public int price;
    public int type;
    
    public float range;
    public float cooldown;
    public float currCooldown = 0;

    public Sprite spr;
    
    public Tower(string _name, float _range, float _cooldown, int _type, int _price)
    {
        name = _name;
        type = _type;
        range = _range;
        cooldown = _cooldown;
        price = _price;

        //spr = Resources.Load<Sprite>(path);
    }
}

public class TowerProjectile
{
    public float speed;
    public int damage;
    //public Sprite spr;

    public TowerProjectile(float _speed, int _damage)
    {
        speed = _speed;
        damage = _damage;
    }
}

public class Enemy
{
    public float startSpeed;
    public float speed;
    public float health;
    public Color color;

    public Enemy(int _health, float _startSpeed, Color _color)
    {
        health = _health;
        startSpeed = _startSpeed;
        speed = _startSpeed;
        color = _color;
    }

    public Enemy(Enemy other)
    {
        health = other.health;
        startSpeed = other.startSpeed;
        speed = other.startSpeed;
        color = other.color;
    }
}

public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER
}

public class GameControllerScr : MonoBehaviour
{
    public GameObject shop;
    
    public List<Tower> allTowers = new List<Tower>();
    public List<TowerProjectile> allTowersProjectiles = new List<TowerProjectile>();
    public List<Enemy> allEnemies = new List<Enemy>();
    private void Awake()
    {
        allTowers.Add(new Tower("fast tower", 2, 0.3f, 0, 10));
        allTowers.Add(new Tower("BigDamage tower",2, 1, 1, 20));
        
        allTowersProjectiles.Add(new TowerProjectile(8, 15));
        allTowersProjectiles.Add(new TowerProjectile(8, 15));
        
        allEnemies.Add(new Enemy(30, 7, new Color(255,0,0)));
        allEnemies.Add(new Enemy(50, 5, new Color(0,0,255)));
    }
}
