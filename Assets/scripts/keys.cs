using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public GameObject chaveUI;
    public GameObject chaveEnemyUI;
    public int keyNumber;

    // Start is called before the first frame update
    public void Start()
    {
        chaveUI.SetActive(false);
        chaveEnemyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerController>().GetKey(keyNumber);
            chaveUI.SetActive(true);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
        else if(collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyBehaviour>().GetKey(keyNumber);
            chaveEnemyUI.SetActive(true);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }
    
}
