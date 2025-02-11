using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public TMP_Text txtCoin;
    public int coin;
    public Button btnRandomBuy, btnAdd;
    private void Start()
    {
        coin = GameManager.instance.coins;
        txtCoin.text = coin.ToString();
        btnAdd.onClick.AddListener(AddCoin);
    }

    public void AddCoin()
    {
        coin += 200;
        txtCoin.text = coin.ToString();
        PlayerPrefs.SetInt("Coins", coin);
    }
    
}
