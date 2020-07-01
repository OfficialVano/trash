using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public GameObject itemPref;
    public Transform itemGrid;

    private GameControllerScr _gcs;

    public CellScr selfCell;

    private void Start()
    {
        _gcs = FindObjectOfType<GameControllerScr>();

        //GObject = GetComponent<GameObject>();
        
        foreach (var tower in _gcs.allTowers)
        {
            var tmpItem = Instantiate(itemPref);
            tmpItem.transform.SetParent(itemGrid,false);
            tmpItem.GetComponent<ShopItem>().SetStartDate(tower, selfCell);
        }
    }

    /*private void Update()
    {
        if(ShopActive)
            OpenShop();
    }*/

    public void CloseShop()
    {
        /*_gcs.shop.SetActive(false);
        ShopActive = false;
        */
        Destroy(gameObject);
    }

    /*public bool ShopActive()
    {
        return _gcs.shop.activeSelf;
    }*/
    
    /*public void OpenShop()
    {
        _gcs.shop.SetActive(true);
    }*/
}
