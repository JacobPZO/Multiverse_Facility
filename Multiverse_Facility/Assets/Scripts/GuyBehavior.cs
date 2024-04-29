using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Broombally Doombally!!!");
            gameManager.Items += 1;

            PlayerBehavior Player = collision.gameObject.GetComponent<PlayerBehavior>();
            Player.ShowGuy();
        }
    }
}
