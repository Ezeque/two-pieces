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
    }
    
}
