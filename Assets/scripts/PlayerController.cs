using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int numChaves;   /* NÚMERO DE CHAVES QUE O PLAYER TEM */
    public float speed;     /* VELOCIDADE DO PLAYER */
    public GameObject Projetil;     /* PREFAB DO PROJETIL */
    public float projetilSpeed;     /* VELOCIDADE DO PROJETIL */
    private bool isClicking;        /* SE O PLAYER JÁ ESTÁ CLICANDO */
    public Sprite[] sprites;   /* ARMAZENA OS SPRITES DO PLAYER */
    public int life;
    private GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        life = 10;
        isClicking = false;
        numChaves = 0;
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        disparar();
        move();
        checaMorte();
    }

    /* GUARDA A LÓGICA DE MOVIMENTAÇÃO DO PERSONAGEM */
    public void move(){
        if(Input.GetButton("Horizontal")){
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (speed * Input.GetAxis("Horizontal")), transform.position.y, transform.position.z), Time.deltaTime);
        }
        if(Input.GetButton("Vertical")){
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + (speed * Input.GetAxis("Vertical")), transform.position.z), Time.deltaTime);          
        }
        resolveSprite();
    }
        
    /* DISPARA OS PROJÉTEIS */
    public void disparar(){
        if(Input.GetButton("Fire1") && isClicking == false){
            isClicking = true;
            Vector3 posicaoMouse = Input.mousePosition;
            posicaoMouse.z = - Camera.main.transform.position.z;
            Vector3 posicaoNoMundo = Camera.main.ScreenToWorldPoint(posicaoMouse);
            posicaoNoMundo.z = -1;
            Vector3 direcao = (posicaoNoMundo - transform.position).normalized;
            GameObject projetilInstanciado = Instantiate(Projetil, transform.position + direcao * 0.5f, Quaternion.identity);
            Rigidbody2D rb = projetilInstanciado.GetComponent<Rigidbody2D>();
            rb.velocity = direcao * projetilSpeed;
        }
        else if(!Input.GetButton("Fire1")){
            isClicking = false;
        }
    }

    /* ATUALIZA OS SPRITES DO BARCO */
    private void resolveSprite(){
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") > 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 1){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
        else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[5];
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[6];
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[7];
        }
    }

    private void checaMorte(){
        if(life == 0){
            gameOverScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
    
}
