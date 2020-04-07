using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{

    public Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void Save() 
    {
        PlayerPrefs.SetInt("coins", player.coins); 
        print("File Prefab Save Successfully: " + PlayerPrefs.GetInt("coins"));
        //local onde salva: (Windows + R, e digita: regedit) HKEY_CURRENT_CONFIG\Software[company name][product name] key
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("coins")) //Se existir a chave com nome de moedas(coins), ele carrega o "save"
        {
            player.coins = PlayerPrefs.GetInt("coins");
            GameObject.FindObjectOfType<Player>().UpdateCoinText();
            print("File Prefab Read Successfully: " + PlayerPrefs.GetInt("coins"));
        }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        GameObject.FindObjectOfType<Player>().UpdateCoinText();
        print("File Prefab Delete Successfully:");
    }
}
