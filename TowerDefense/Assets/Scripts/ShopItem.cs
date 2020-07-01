using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private Tower _selfTower;
    private CellScr _selfCell;
    
    public Image TowerLogo;
    public Text TowerName;
    public Text TowerPrice;

    public Color baseColor;
    public Color currColor;
    
    public void SetStartDate(Tower tower, CellScr cell)
    {
        _selfTower = tower;
        //TowerLogo.sprite = _selfTower.spr;
        TowerName.text = tower.name;
        TowerPrice.text = tower.price.ToString();
        _selfCell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoneyManager.instance.gameMoney >= _selfTower.price)
        {
            _selfCell.BuildTower(_selfTower);
            MoneyManager.instance.gameMoney -= _selfTower.price;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = currColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = baseColor;
    }
}
