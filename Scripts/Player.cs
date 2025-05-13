using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//herencia de MonoBehavior
{
    //VARIABLES
    [SerializeField]
    private GameObject laserPrefab;// Objeto para usar el prefab en el jugador
    [SerializeField]
    private GameObject tripleShootPrefab;// Objeto para usar el prefab en el jugador
    [SerializeField]
    private GameObject shieldPrefab;// Objeto para usar el prefab en el jugador
    [SerializeField]
    private float timeShoot = 0.25f; //es el tiempo que dura un disparo
    [SerializeField]
    private GameObject _playerExplotion; // Para prefab de la explosion
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;
    public GameManager _gameManager;//Pantalla de titulo

    public float speed = 250f;
    private float nextShoot = 0.0f;//para contar desde cero hasta el proximo disparo

    public bool tripleShoot = false;
    public bool shield = false;
    public int shieldDamage = 0;
    public int vidas = 3;
    public int hit_count = 3;
    public int engine;

    private UIManager ui_Manager;
    // instanciar spawn manager
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
       // current pos = new position
       transform.position = new Vector3(466,265,0);

       ui_Manager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (ui_Manager != null){
            ui_Manager.UpdateLives(vidas);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        // Verificar que se encontro el spawn manager
        if (_spawnManager != null){
            // Si encontro el spawn manager
            _spawnManager.StartSpawnRoutine();
        }

        _audioSource = GetComponent<AudioSource>(); // Inicializando el audio
        
        // hit COUNT
        hit_count = 0;
    }

    // El codigo en Update se ejecuta una vez por frame
    void Update()
    {
        Movimiento();
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Disparar();
        }
    }

    //FUNCIONES
    private void Movimiento(){
        float horizontalInput= Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //Vector3(1,0,0) * 1 * speed
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);//Time.deltaTime nos da el tiempo en fps para el juego
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        //RESTRINGIR EN Y
        if (transform.position.y > 573){
            transform.position = new Vector3(transform.position.x,-50,0);//(Posicion de x , posision de y , posicion de z)               
        }else if(transform.position.y < -50f){
            transform.position = new Vector3(transform.position.x , 573f , 0);//(Posicion de x , posision de y , posicion de z)                            
        }
        //RESTRINGIR POSISION EN X
        if (transform.position.x > 973f){
            transform.position = new Vector3((-33f) , transform.position.y , 0); //(Posicion de x , posision de y , posicion de z)    
        }else if(transform.position.x < -33f){
            transform.position = new Vector3((973f) , transform.position.y ,0);//(Posicion de x , posision de y , posicion de z)                    
        }
    }

    private void Disparar(){  
        if (Time.time > nextShoot){
            _audioSource.Play();
            if (tripleShoot){ 
                //Si se puede disparar Crear el laser
                Instantiate(tripleShootPrefab, transform.position,Quaternion.identity);
                nextShoot = Time.time + timeShoot;// para que se sume el tiempo que dura el disparo y se bloquee por ese tiempo
            }else{
                //Si se puede disparar Crear el laser
                Instantiate(laserPrefab, transform.position + new Vector3(0, 30f,0) ,Quaternion.identity);//se coloca la posicion del jugador con transform.position y se le suma el desplazamiento en y
                nextShoot = Time.time + timeShoot;// para que se sume el tiempo que dura el disparo y se bloquee por ese tiempo
            }
        }
    }

    /*----------PARA LOS POWER UPS---------------*/

    // Damage system
    public void Damage(){
        engine = Random.Range(0, 2); 
        if (shield == true){
            shield = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        else{
            hit_count++;
            if (hit_count==1){
                // turn on random damage_engine animation
                _engines[engine].SetActive(true);
            }
            else if (hit_count==2){
                // turn on the other damage engine animation
                if (_engines[0].activeSelf){
                    _engines[1].SetActive(true);
                }else{
                    _engines[0].SetActive(true);
                }
            }

            // subtract 1 life from player
            vidas--;
            ui_Manager.UpdateLives(vidas);
        }
        // if lives <1 (meaning 0)
        if (vidas < 1){
            Instantiate(_playerExplotion, transform.position, Quaternion.identity); // Hacer que se instancie una explosion en la posision actual
            Destroy(this.gameObject);
            _gameManager.gameOver = true;
            ui_Manager.ShowTitleScreen();
        }
    }

    //Inicio de sistema de COUNT DOWN
    public void TripleShootPowerUpOn(){
        tripleShoot =true;
        timeShoot = 0.0f;
        StartCoroutine(TripleShootPowerDownRoutine());
    }

    public IEnumerator TripleShootPowerDownRoutine(){
        yield return new WaitForSeconds(10.0f);
        timeShoot = 0.25f;
        tripleShoot = false;
    }
    //Fin de sistema de COUNT DOWN

    public void SpeedPowerUpOn(){
        speed = speed * 3;
        timeShoot = 0.0f;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public IEnumerator SpeedPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        speed = 250.0f;
    }

    // Power Up de escudo
    public void ShieldPowerUpOn(){
        shield = true;
        _shieldGameObject.SetActive(true);
    }
}


