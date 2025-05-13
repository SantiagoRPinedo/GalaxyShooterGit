using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // IF a key is pressed or left arrow is pressed
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow))){

            _anim.SetBool("Turn_left",true);
            _anim.SetBool("Turn_Right",false);
        }else if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.LeftArrow))){
            _anim.SetBool("Turn_left",false);
            _anim.SetBool("Turn_Right",false);
        }

        // IF d key is pressed or Right arrow is pressed
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow))){
            _anim.SetBool("Turn_left",false);
            _anim.SetBool("Turn_Right",true);
        }else if (Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.RightArrow))){
            _anim.SetBool("Turn_left",false);
            _anim.SetBool("Turn_Right",false);
        }

    }
}
