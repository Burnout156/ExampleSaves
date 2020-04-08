using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int velocity;
    public int coins;
    public float x;
    public float y;
    public Rigidbody2D rigidBody;
    public bool IsAutoSave;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (velocity <= 0)
        {
            velocity = 200;
        }

        UpdateCoinText();

        if (IsAutoSave) //se estiver marcado o autosave antes de jogar, será criado um save automaticamente
        {
            if (!File.Exists(JsonExample.filePath)) //se o arquivo não existir, criará o primeiro save
            {
                JsonExample.AutoSaving();
            }

            else
            {
                JsonExample.AutoLoading();
                JsonExample.MainSave.isAutoSave = IsAutoSave;
            }
        }

        if (File.Exists(JsonExample.filePath)) //se o autosave não estiver marcado, mas estiver salvo no arquivo, ele pegará
        {
            JsonExample.AutoLoading();
            IsAutoSave = JsonExample.MainSave.isAutoSave;
        }
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        JsonExample.MainSave.isAutoSave = IsAutoSave;

        Move(x);
    }

    public void Move(float x)
    {
        if (x != 0 || y != 0)
        {
            rigidBody.velocity = new Vector2(x, y) * Time.deltaTime * velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            coins++;

            if (IsAutoSave) //se tiver marcado o auto save, os dados são salvos automaticamente quando coleto uma moeda
            {
                if (!File.Exists(JsonExample.filePath)) //se o arquivo não existir, criará o primeiro save
                {
                    JsonExample.AutoSaving();
                    JsonExample.MainSave.player.coins = coins;
                    JsonExample.AutoSaving();
                }

                else
                {
                    JsonExample.MainSave.player.coins = coins;
                    JsonExample.AutoSaving();
                }
            }

            else //se o autosave estiver desmarcado,  ele salvará o autosave desmarcado
            {
                JsonExample.MainSave.isAutoSave = IsAutoSave;
                JsonExample.AutoSaving();
            }

            Destroy(collision.gameObject);
            UpdateCoinText();
        }
    }

    public void UpdateCoinText()
    {
        GameObject.Find("CoinText").GetComponent<Text>().text = "Coin: " + coins;
    }
}
