using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    //Variables
    [SerializeField]
    private GameObject _enemyExplotionPrefab;

    [SerializeField]
    private float speed=200.0f;

    //Referencia al Ui manager
    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        Movimiento();
    }

    //Funciones 
    private void Movimiento(){
        //mover par abajao
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //Verificar si se salio de la pantalla
        if (transform.position.y < -37.0f){
            SpawnEnemigo();
        }
    }
    private void SpawnEnemigo(){
        float posicionX =Random.Range(34f, 890f);
        // Establecer la posición del objeto arriba de la pantalla en la posición aleatoria de x
        transform.position = new Vector3(posicionX, 558f , 0f);
    }

    /*Para la animacion de explosion*/
    

    //PARA CONTROL DE DAÑO
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag =="Player"){ // Colosion con el jugador
            //Acceder al jugador
            Player player = other.GetComponent<Player>(); //Da acceso a metodos y variables publicas de player
            if (player != null){
                player.Damage();
            }
        }else if(other.tag =="Laser"){ // Colision con laser
            if (other.transform.parent != null){ // destruir lacer triple
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity); // Hacer que se instancie en la posision actual
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip,Camera.main.transform.position,1f);
        }
        Instantiate(_enemyExplotionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_clip,Camera.main.transform.position,1f);
        Destroy(this.gameObject); // Colision con jugador - Destruir enemigo
    }
}
