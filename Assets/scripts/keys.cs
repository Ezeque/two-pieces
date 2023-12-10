using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public GameObject chaveUI;

    // Start is called before the first frame update
    void Start()
    {
        chaveUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerController>().numChaves ++;
            chaveUI.SetActive(true);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyBehaviour>().cacandoChave = false;
            collision.gameObject.GetComponent<EnemyBehaviour>().numChaves ++;
            collision.gameObject.GetComponent<EnemyBehaviour>().chaveUm = GameObject.Find("Chave 1");
            collision.gameObject.GetComponent<EnemyBehaviour>().chaveDois = GameObject.Find("Chave 2");
            collision.gameObject.GetComponent<EnemyBehaviour>().criaAlvoAleatorio();
            chaveUI.SetActive(true);
            Destroy(gameObject);
        }
    }
    
}
