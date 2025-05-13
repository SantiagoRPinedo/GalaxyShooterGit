using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float speed= 150f;
    [SerializeField]
    private int powerupID; //0=disparo triple, 1= speedbost, 2= escudo
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -7){
            Destroy(this.gameObject);
        }        
    }

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Collided width: " + other.name);
        
        if(other.tag =="Player"){
            //Acceder al jugador
            Player player = other.GetComponent<Player>(); //Da acceso a metodos y variables publicas\
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position,1f);
            if (player != null){
                if (powerupID == 0){
                    //Cambiar el booleando del disparo triple a verdadero
                    player.TripleShootPowerUpOn();
                }else if (powerupID == 1){
                    //Habilitar velocidad 
                    player.SpeedPowerUpOn();
                }else if (powerupID == 2){
                    //Habilitar escudo 
                    player.ShieldPowerUpOn();
                }
            }
            //Destruir al power up
            Destroy(this.gameObject);
        }
    }

}
