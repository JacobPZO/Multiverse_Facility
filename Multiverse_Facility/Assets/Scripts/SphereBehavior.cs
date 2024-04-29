using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
    public GameBehavior gameManager;
    public float BoostMultiplier = 1.5f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Orb obtained! Speed Permanently increased!");

            PlayerBehavior Player = collision.gameObject.GetComponent<PlayerBehavior>();
            Player.BoostSpeed(BoostMultiplier);

            gameManager.Items += 1;
            gameManager.PrintLootReport();
        }
    }
}