using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellScr : MonoBehaviour
{
    //private GameControllerScr _gcs;
    
    public bool isGround;
    public bool hasTower;

    public Color baseColor;
    public Color currColor;

    public GameObject shopPref;
    public GameObject towerPref;

    private void OnMouseEnter()
    {
        if (!isGround) 
            if (FindObjectsOfType<Shop>().Length == 0)
                GetComponent<SpriteRenderer>().color = currColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    private void OnMouseDown()
    {
        if(!isGround && FindObjectsOfType<Shop>().Length == 0)
            if (!hasTower)
            {
                var tmpShop = Instantiate(shopPref);
                tmpShop.transform.SetParent(GameObject.Find("Canvas").transform, false);
                tmpShop.GetComponent<Shop>().selfCell = this;
            }
    }

    public void BuildTower(Tower tower)
    {
        var tmpTower = Instantiate(towerPref);
        tmpTower.transform.SetParent(transform,false);
        var towerPos = new Vector2(transform.position.x + tmpTower.GetComponent<SpriteRenderer>().bounds.size.x/2,
                                   transform.position.y - tmpTower.GetComponent<SpriteRenderer>().bounds.size.y/2);
        tmpTower.transform.position = towerPos;
        
        tmpTower.GetComponent<TowerScr>().selfType = (TowerType) tower.type;
        
        hasTower = true;
        FindObjectOfType<Shop>().CloseShop();
    }
}
