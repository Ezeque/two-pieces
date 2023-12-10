using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{
    Vector2 novaPosicao;
    public Vector2 worldBottomLeft;
    public Vector2 worldTopRight;
    private GameObject alvo;
    private GameObject player;
    public GameObject chaveUm;
    public GameObject chaveDois;
    private bool cacandoPlayer = false;
    public bool cacandoChave = false;
    public int numChaves;
    // Start is called before the first frame update
    void Start()
    {
        numChaves = 0;
        chaveUm = GameObject.Find("Chave 1");
        chaveDois = GameObject.Find("Chave 2");
        player = GameObject.Find("Player");
        novaPosicao = getRandomMapPoint();
        alvo = new GameObject("target");
        GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
    }

    // Update is called once per frame
    void Update()
    {
        checaChavePerto();
        checaPlayerPerto();
        if(Vector2.Distance((Vector2)transform.position, novaPosicao) < 1f && !cacandoPlayer){
            novaPosicao = getRandomMapPoint();
            GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
        }
    }

    Vector2 getRandomMapPoint(){
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
        return novaPosicao;
    }

    GameObject criaAlvo(Vector2 posicao){
        alvo.transform.position = posicao;
        return alvo;
    }

    void checaPlayerPerto(){
        if(Vector2.Distance((Vector2)transform.position, player.transform.position) < 5f && !cacandoChave){
            cacandoPlayer = true;
            alvo.transform.position = new Vector2(player.transform.position.x - 1, player.transform.position.y - 1);
            GetComponent<AIDestinationSetter>().target = alvo.transform;
        }
        else{
            cacandoPlayer = false;
        }
    }

    void checaChavePerto(){
        if(GameObject.Find("Chave 1") != null){
                if(Vector2.Distance((Vector2)transform.position, chaveUm.transform.position) < 5f && !cacandoChave){
                cacandoChave = true;
                alvo.transform.position = new Vector2(chaveUm.transform.position.x, chaveUm.transform.position.y);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }
        if(GameObject.Find("Chave 2") != null){
            if(Vector2.Distance((Vector2)transform.position, chaveDois.transform.position) < 5f && !cacandoChave){
            Debug.Log("Achou Chave 2");
            cacandoChave = true;
            alvo.transform.position = new Vector2(chaveDois.transform.position.x, chaveDois.transform.position.y);
            GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }
    }

    public void criaAlvoAleatorio(){
        novaPosicao = getRandomMapPoint();
        GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
    }
}
