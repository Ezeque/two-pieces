using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectiles : MonoBehaviour
{
    public GameObject shooter;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject != shooter)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerController>().LoseLife();
                Destroy(gameObject);
            }
            else if(collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyBehaviour>().LoseLife();
                Destroy(gameObject);
            }   
        }
    }
}
