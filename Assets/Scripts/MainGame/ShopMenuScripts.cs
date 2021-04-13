using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ShopMenuScripts : MonoBehaviour
{
    public GameObject currency;
    public Transform player;

    private float dmg;
    private float speed;
    private float heal;

    private void Start()
    {
        dmg = player.GetComponent<DamagePotion>().price;
        speed = player.GetComponent<SpeedPotion>().price;
        heal = player.GetComponent<HealPotion>().price;

    }

    public void DamageClick()
    {
        if (SystemVariables.playerData.money >= dmg)
        {
            SystemVariables.playerData.damagePotions += 1;
            SystemVariables.playerData.money -= dmg;
            currency.GetComponent<Text>().text = SystemVariables.playerData.money.ToString() + " Jz";

            SaveDataScript.SaveGame();
        }
    }

    public void SpeedClick()
    {
        if (SystemVariables.playerData.money >= speed)
        {
            SystemVariables.playerData.speedPotions += 1;
            SystemVariables.playerData.money -= speed;
            currency.GetComponent<Text>().text = SystemVariables.playerData.money.ToString() + " Jz";
            SaveDataScript.SaveGame();
        }
    }

    public void HealClick()
    {
        if (SystemVariables.playerData.money >= heal)
        {
            SystemVariables.playerData.healingPotions += 1;
            SystemVariables.playerData.money -= heal;
            currency.GetComponent<Text>().text = SystemVariables.playerData.money.ToString() + " Jz";
            SaveDataScript.SaveGame();
        }
    }
}
