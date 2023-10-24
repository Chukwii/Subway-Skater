using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject hatShop, magicianHat, cowboyHat, sombreroHat, hatPrent;
    public int price;
    private bool hasBoughtMagic, hasBoughtCowboy, hasBoughtSombrero;
    public Text magicTxt, cowboyTxt, sombreTxt;
    public bool shopActive;

    private void Start()
    {
        if (PlayerPrefs.GetInt("magicBought") == 1)
        {
            magicTxt.text = "Bought";
            hasBoughtMagic = true;
        }
            
        if(PlayerPrefs.GetInt("cowboyBought") == 1)
        {
            cowboyTxt.text = "Bought";
            hasBoughtCowboy = true;
        }
            
        if(PlayerPrefs.GetInt("sombreroBought") == 1)
        {
            sombreTxt.text = "Bought";
            hasBoughtSombrero = true;
        }
    }

    public void OpenHatShop()
    {
        hatShop.SetActive(true);
        GameManager.Instance.gameCanvas.SetTrigger("Show");
        shopActive = true;
    }

    public void CloseHatShop()
    {
        hatShop.SetActive(false);
        GameManager.Instance.gameCanvas.SetTrigger("Hide");
        shopActive = false;
    }

    public void BuyMagicianHat()
    {
        price = 100;
        if (price <= GameManager.Instance.coinScore && !hasBoughtMagic)
        {
            GameManager.Instance.coinScore -= price;
            magicianHat.SetActive(true);
            cowboyHat.SetActive(false);
            sombreroHat.SetActive(false);
            hasBoughtMagic = true;
            magicTxt.text = "Bought";
            PlayerPrefs.SetInt("magicBought", 1);
        }
        else if (hasBoughtMagic)
        {
            magicianHat.SetActive(true);
            cowboyHat.SetActive(false);
            sombreroHat.SetActive(false);
            PlayerPrefs.SetInt("magicBought", 0);
        }
            
    }

    public void BuyCowboyHat()
    {
        price = 500;
        if (price <= GameManager.Instance.coinScore && !hasBoughtCowboy)
        {
            GameManager.Instance.coinScore -= price;
            cowboyHat.SetActive(true);
            magicianHat.SetActive(false);
            hasBoughtCowboy = true;
            sombreroHat.SetActive(false);
            cowboyTxt.text = "Bought";
            PlayerPrefs.SetInt("cowboyBought", 1);
        }
        else if (hasBoughtCowboy)
        {
            cowboyHat.SetActive(true);
            magicianHat.SetActive(false);
            sombreroHat.SetActive(false);
            PlayerPrefs.SetInt("cowboyBought", 0);
        }
    }

    public void BuySombreroHat()
    {
        price = 1000;
        if (price <= GameManager.Instance.coinScore && !hasBoughtSombrero)
        {
            GameManager.Instance.coinScore -= price;
            sombreroHat.SetActive(true);
            magicianHat.SetActive(false);
            cowboyHat.SetActive(false);
            hasBoughtSombrero = true;
            PlayerPrefs.SetInt("sombreroBought", 1);
            sombreTxt.text = "Bought";
        }
        else if (hasBoughtSombrero)
        {
            sombreroHat.SetActive(true);
            magicianHat.SetActive(false);
            cowboyHat.SetActive(false);
            PlayerPrefs.SetInt("sombreroBought", 0);
        }
    }

    public void DisableHatSelected()
    {
        magicianHat.SetActive(false);
        cowboyHat.SetActive(false);
        sombreroHat.SetActive(false);
    }
}
