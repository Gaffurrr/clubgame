using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemSO[] shopItemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;
    public Button[] businessBtn;

    void Start()
    {
        for(int i=0; i< shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);
        coinUI.text = "Para: " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();
    }

    void Update()
    {

    }
    public void AddCoins()                           
    {
        coins++;
        coinUI.text = "Para: " + coins.ToString();
        CheckPurchaseable();
    }
    public void CheckPurchaseable()                       //satin alinabilir mi?
    {
        for (int i=0; i<shopItemsSO.Length; i++)
        {
            if (coins >= shopItemsSO[i].baseCost)           //eger yeterli param varsa
                myPurchaseBtns[i].interactable = true;
            else
                myPurchaseBtns[i].interactable = false;
        }
    }
    public void PurchaseItem(int BtnNo)                    //Buton numaralarina gore satin aliyor
    {
        if(coins >= shopItemsSO[BtnNo].baseCost)
        {
            coins = coins - shopItemsSO[BtnNo].baseCost;       //para - alinan seyin bedeli
            coinUI.text = "Para: " + coins.ToString();         //yeni para degerinin girilmesi
            CheckPurchaseable();
        }
    }
    public void LoadPanels()
    {
        for(int i= 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = "Fiyat: " + shopItemsSO[i].baseCost.ToString();
        }
    }
}
