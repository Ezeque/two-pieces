using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
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
}
