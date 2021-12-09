using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManBasicMove : MonoBehaviour
{   

    //Velocidad de movimiento
    public float velocidad = 4.0f;

    //Direccion del movimiento
    private Vector2 direccion;

    void Start()
    {   
        //La direccion inicial es el vector de ceros
        direccion = Vector2.zero;
    }

    void Update()
    {   
        //En cada frame se lee el movimiento del jugador
        obtenerDireccion();

        //Se plica el movimiento
        transform.localPosition += (Vector3)(direccion*velocidad)*Time.deltaTime;
        
    }

    //se obtiene la direccion del movimiento del usuario con las teclas WASD o las flechas
    void obtenerDireccion(){
        if(Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)){
            direccion = Vector2.up;
        } else if(Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)){
            direccion = Vector2.down;
        } else if(Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)){
            direccion = Vector2.left;
        } else if(Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)){
            direccion = Vector2.right;
        }
    }
}
