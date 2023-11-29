using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    
    public Vector2 worldBottomLeft;
    public Vector2 worldTopRight;
    // Start is called before the first frame update
    void Start()
    {
        randomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* REALIZA O SPAWN ALEATÓRIO */
    private void randomSpawn(){
        float minX = worldBottomLeft.x;
        float maxX = worldTopRight.x;
        float minY = worldBottomLeft.y;
        float maxY = worldTopRight.y;
        bool isValidPosition = false;
        float newY = 0;
        float newX = 0;
        Vector2 novaPosicao;

        while(!isValidPosition){
            newY = Random.Range(minY, maxY);
            newX = Random.Range(minX, maxX);
            
            Collider2D colisorNaPosicao = Physics2D.OverlapPoint(new Vector2(newX, newY), 1 << LayerMask.NameToLayer("Island"));
            if(colisorNaPosicao == null){
                isValidPosition = true;
            }
        }
        novaPosicao = new Vector2(newX, newY);
        transform.position = new Vector3(novaPosicao.x, novaPosicao.y, -1);
    }
}
