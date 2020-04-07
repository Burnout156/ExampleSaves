using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int velocity;
    public int coins;
    public float x;
    public float y;
    public Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if(velocity <= 0)
        {
            velocity = 200;
        }

        UpdateCoinText();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

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
            Destroy(collision.gameObject);
            UpdateCoinText();
        }
    }

    public void UpdateCoinText()
    {
        GameObject.Find("CoinText").GetComponent<Text>().text = "Coin: " + coins;
    }
}
