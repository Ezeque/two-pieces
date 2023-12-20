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
    [SerializeField]private bool cacandoNavio = false;
    public bool cacandoChave = false;
    public bool noKeyInMap = false;
    public bool goForWin = false;
    public bool keyOne;
    public bool keyTwo;
    public int life;
    public GameObject enemyPrefab;
    public GameObject Projetil; 
    public float shootDelay;
    private float cooldown;
    public float projetilSpeed;  
    public GameObject enemyWithKeyOne;
    public GameObject enemyWithKeyTwo;
    public GameObject enemyTarget;
    public GameObject bauDeMariz;

    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        keyOne = false;
        keyTwo = false;
        cooldown = 0;
        chaveUm = GameObject.Find("Chave 1");
        chaveDois = GameObject.Find("Chave 2");
        player = GameObject.Find("Player");
        novaPosicao = getRandomMapPoint();
        alvo = new GameObject("target");
        bauDeMariz = GameObject.Find("Bau");
        GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        } 
        else
        {
            cooldown = 0;
        }
        // if(Vector2.Distance((Vector2)transform.position, player.transform.position) < 5f && cacandoPlayer && cooldown == 0){
        //     ShootTarget(player.transform.position);
        // }

        if(cacandoPlayer && cooldown == 0){
            ShootTarget(player.transform.position);
        }
        else if(cacandoNavio && cooldown == 0)
        {
            ShootTarget(enemyTarget.transform.position);
        }

        if(goForWin == false)
        {
            checaChavePerto();
            if(noKeyInMap)
            {
                checaNavioPerto();
            }
            checaPlayerPerto();
            if(Vector2.Distance((Vector2)transform.position, novaPosicao) < 1f && !cacandoPlayer){
                novaPosicao = getRandomMapPoint();
                GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
            }
        }

        checaMorte();
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
        if(!cacandoChave && !cacandoNavio && Vector2.Distance((Vector2)transform.position, player.transform.position) < 5f){
            cacandoPlayer = true;
            alvo.transform.position = new Vector2(player.transform.position.x - 1, player.transform.position.y - 1);
            GetComponent<AIDestinationSetter>().target = alvo.transform;
        }
        else{
            cacandoPlayer = false;
        }
    }

    void checaNavioPerto(){
        GameObject[] listanavios;

        enemyWithKeyOne = gameObject;
        enemyWithKeyTwo = gameObject;

        listanavios = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject ship in listanavios)
        {
            if(ship.GetComponent<EnemyBehaviour>().keyOne)
            {
                enemyWithKeyOne = ship;
            }
            if(ship.GetComponent<EnemyBehaviour>().keyTwo)
            {
                enemyWithKeyTwo = ship;
            }
        }

        if(enemyWithKeyOne != gameObject)
        {
            if(!cacandoNavio && Vector2.Distance((Vector2)transform.position, enemyWithKeyOne.transform.position) < 5f){
                enemyTarget = enemyWithKeyOne;
                cacandoNavio = true;
                alvo.transform.position = new Vector2(enemyWithKeyOne.transform.position.x - 1, enemyWithKeyOne.transform.position.y - 1);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }

        if(enemyWithKeyTwo != gameObject){
            if(!cacandoNavio && Vector2.Distance((Vector2)transform.position, enemyWithKeyTwo.transform.position) < 5f){
                enemyTarget = enemyWithKeyTwo;
                cacandoNavio = true;
                alvo.transform.position = new Vector2(enemyWithKeyTwo.transform.position.x - 1, enemyWithKeyTwo.transform.position.y - 1);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }
    }

    void checaChavePerto(){
        if(GameObject.Find("Chave 1") != null){
            noKeyInMap = false;
            cacandoNavio = false;
            if(!cacandoChave && Vector2.Distance((Vector2)transform.position, chaveUm.transform.position) < 5f){
                cacandoChave = true;
                alvo.transform.position = new Vector2(chaveUm.transform.position.x, chaveUm.transform.position.y);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }
        if(GameObject.Find("Chave 2") != null){
            noKeyInMap = false;
            cacandoNavio = false;
            if(!cacandoChave && Vector2.Distance((Vector2)transform.position, chaveDois.transform.position) < 5f){
                cacandoChave = true;
                alvo.transform.position = new Vector2(chaveDois.transform.position.x, chaveDois.transform.position.y);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
        }
        else if(GameObject.Find("Chave 1") == null)
        {
            if(keyOne && keyTwo)
            {
                goForWin =  true;
                alvo.transform.position = new Vector2(bauDeMariz.transform.position.x, bauDeMariz.transform.position.y);
                GetComponent<AIDestinationSetter>().target = alvo.transform;
            }
            else
            {
                noKeyInMap = true;
            }
        }
    }

    public void criaAlvoAleatorio(){
        novaPosicao = getRandomMapPoint();
        GetComponent<AIDestinationSetter>().target = criaAlvo(novaPosicao).transform;
    }

    public void checaMorte(){
        if(life <= 0){
            if(keyOne)
            {
                keyOne = false;
                GameObject key = gameObject.transform.GetChild(0).gameObject;
                key.SetActive(true);
                key.transform.SetParent(null);
                //key.GetComponent<RandomSpawn>().randomSpawn();
                key.GetComponent<keys>().Start();
            }
            if(keyTwo)
            {
                keyTwo = false;
                GameObject key = gameObject.transform.GetChild(0).gameObject;
                key.SetActive(true);
                key.transform.SetParent(null);
                //key.GetComponent<RandomSpawn>().randomSpawn();
                key.GetComponent<keys>().Start();
            }
            GameObject.Instantiate(enemyPrefab);
            Destroy(gameObject);
        }
    }

    void ShootTarget(Vector3 target)
    {
        Vector3 randomVector = new Vector3();
        Vector3 shootingPosition = target + randomVector;
        shootingPosition.z = - Camera.main.transform.position.z;
        Vector3 direcao = (shootingPosition - transform.position).normalized;
        GameObject projetilInstanciado = Instantiate(Projetil, transform.position + direcao * 0.5f, Quaternion.identity);
        projetilInstanciado.GetComponent<Projectiles>().shooter = this.gameObject;
        Rigidbody2D rb = projetilInstanciado.GetComponent<Rigidbody2D>();
        rb.velocity = direcao * projetilSpeed;

        cooldown = shootDelay;
    }

    public void LoseLife()
    {
        this.life--;
        // if(keyOne)
        // {
        //     keyOne = false;
        // }
        // else if(keyTwo)
        // {
        //     keyTwo = false;
        // }
    }

    public void GetKey(int keyNumber)
    {
        cacandoChave = false;
        if(keyNumber == 1)
        {
            keyOne = true;
        }
        else if(keyNumber == 2)
        {
            keyTwo = true;
        }

        chaveUm = GameObject.Find("Chave 1");
        chaveDois = GameObject.Find("Chave 2");
        criaAlvoAleatorio();
    }
}
