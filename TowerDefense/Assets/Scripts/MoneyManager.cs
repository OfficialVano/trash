using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public Text moneyText;
    public int gameMoney;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        moneyText.text = gameMoney.ToString( );
    }
}
