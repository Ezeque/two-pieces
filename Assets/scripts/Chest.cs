using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.Find("GameOverScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            if(collision.gameObject.GetComponent<EnemyBehaviour>().keyOne && collision.gameObject.GetComponent<EnemyBehaviour>().keyTwo)
            {
                gameOverScreen.SetActive(true);
            }
        }
        else if(collision.gameObject.CompareTag("Player")){
            if(collision.gameObject.GetComponent<PlayerController>().keyOne && collision.gameObject.GetComponent<PlayerController>().keyTwo)
            {
                Debug.Log("Você ganhou!");
            }
        }
    }
}
