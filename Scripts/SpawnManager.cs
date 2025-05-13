using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Variables game object para guardar los objetos del juego
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;


    // Start is called before the first frame update
    // Sirve para inicializar variables, metodos, cosas que corren al iniciar el juego 
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpsRoutine());
    }

    // Metodo para iniciar el spawn de las cosas
    public void StartSpawnRoutine(){
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpsRoutine());
    }

    // Co-rutina para hacer spawn de enemigos cada 5 segundos
    IEnumerator EnemySpawn(){
        while(_gameManager.gameOver == false){
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(34, 890f),557,0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerUpsRoutine(){
        while(_gameManager.gameOver == false){
            int randomPowerUp = Random.Range(0,3); // Power Up aleatorio de los 3 disponibles
            Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(34, 890f),557,0), Quaternion.identity); // Instanciar el powerup 
            yield return new WaitForSeconds(10.0f);
        }
    }

}
