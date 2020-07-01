using System.Collections;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LvlManagerScr : MonoBehaviour
{
    public int fieldWidth;
    public int fieldHeight;

    //public GameObject xer;
    
    public GameObject cellPref;
    public Transform cellParent;
    
    public Sprite[] titleSpr = new Sprite[2];

    public List<GameObject> wayPoints = new List<GameObject>();

    private GameObject[,] allCells = new GameObject[10,18];
    
    private int _currWayX;
    private int _currWayY;

    private GameObject _firstCell;
    
    private void Awake()
    {
        CreateLvl();
        LoadWayPoints();
    }

    private void CreateLvl()
    {
        var worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        
        for(var i=0; i < fieldHeight;i++)
        for (var j = 0; j < fieldWidth; j++)
        {
            var sprIndex = int.Parse(LoadLvlText(1)[i].ToCharArray()[j].ToString());
            var spr = titleSpr[sprIndex];

            var isGround = sprIndex == 1;
            Debug.Log(isGround.ToString());
            CreateCell(j, i, worldVec, spr, isGround);
        }
    }
    
    private void CreateCell(int x, int y, Vector3 worldVec, Sprite spr, bool isGround)
    {
        var tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;
        
        var sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        var sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;
        
        tmpCell.transform.position = new Vector3(worldVec.x + (sprSizeX*x),worldVec.y + (sprSizeY*-y));

        /*if (isGround)
        {*/
            tmpCell.GetComponent<CellScr>().isGround = isGround;
            
            if (_firstCell == null)
            {
                _firstCell = tmpCell;
                
                _currWayX = x;
                _currWayY = y;
                
                wayPoints.Add(_firstCell);
                //Debug.Log("start " + _firstCell.transform.position.x+","+_firstCell.transform.position.y);
            }
        /*}*/
        
        allCells[y, x] = tmpCell;
    }

    private string[] LoadLvlText(int LvlNumber)
    {
        var tmpTxt = Resources.Load<TextAsset>("Lvl" + LvlNumber + "Ground");

        var tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);

        return tmpStr.Split('!');
    }

    private void LoadWayPoints()
    {
        GameObject currWayGO;
        
        

        while (true)
        {
            currWayGO = null;

            if (_currWayX > 0 && allCells[_currWayY, _currWayX - 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[_currWayY, _currWayX - 1]))
            {
                currWayGO = allCells[_currWayY, _currWayX - 1];
                _currWayX--;

                //Debug.Log("left " + currWayGO.transform.position.x+","+currWayGO.transform.position.y);
            }
            else if (_currWayX < (fieldWidth - 1) && allCells[_currWayY, _currWayX + 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[_currWayY, _currWayX + 1]))
            {
                currWayGO = allCells[_currWayY, _currWayX + 1];
                _currWayX++;
                
                //Debug.Log("right " + currWayGO.transform.position.x+","+currWayGO.transform.position.y);
            }
            else if (_currWayY > 0 && allCells[_currWayY-1, _currWayX].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[_currWayY - 1, _currWayX]))
            {
                currWayGO = allCells[_currWayY - 1, _currWayX];
                _currWayY--;
                
                //Debug.Log("Up " + currWayGO.transform.position.x+","+currWayGO.transform.position.y);
            }
            else if (_currWayY < (fieldHeight -1) && allCells[_currWayY +1, _currWayX].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[_currWayY + 1, _currWayX]))
            {
                currWayGO = allCells[_currWayY +1, _currWayX];
                _currWayY++;
                
                //Debug.Log("down " + currWayGO.transform.position.x+","+currWayGO.transform.position.y);
            }
            else
                break;
            
            wayPoints.Add(currWayGO);
        }
    }
}
