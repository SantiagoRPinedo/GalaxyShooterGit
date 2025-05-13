using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //VARIABLES
    [SerializeField]
    private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);//Para trasladar el objeto laser

        if (transform.position.y > 5.42){
            Destroy(this.gameObject,.5f);//Para destruir el objeto cuando salga de la pantalla
        }
    }
}
