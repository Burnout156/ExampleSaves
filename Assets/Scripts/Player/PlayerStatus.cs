using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//criei essa classe para serializar as variáveis do jogador, 
//pois não dá pra fazer isso com classe que tem MonoBehavior
[System.Serializable] 
public class PlayerStatus
{
    public int coins;

    public PlayerStatus(int coin)
    {
        coins = coin;
    }
}
