using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    //Velocidad de movimiento
    public float velocity = 4.0f;
    public GameObject _PacManBody;

    //Direccion del movimiento
    private Vector3 direction;
    Rigidbody _rb;
    public static int score = 0;

    void Start()
    {   
        //La direccion inicial es el vector de ceros
        direction = Vector3.zero;
        _rb = GetComponent<Rigidbody>();
        _PacManBody = GameObject.Find("PacManBody");
    }

    void Update()
    {   
        //En cada frame se lee el movimiento del jugador
        GetDirection();
        //Se plica el movimiento  
    }

    void FixedUpdate()
    {
        move();
    }

    //se obtiene la direccion del movimiento del usuario con las teclas WASD o las flechas
    void GetDirection()
    {
        if(Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)){
            direction = Vector3.forward;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,90);

        } else if(Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)){
            direction = Vector3.back;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,270);

        } else if(Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)){
            direction = Vector3.left;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,180);
             
        } else if(Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)){
            direction = Vector3.right;   
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,0);
        }

        _PacManBody.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90,0,90);
        
    }        

    void move()
    {    
        Vector3 _targetdirection = direction*velocity*Time.deltaTime;

        Vector3 _targetposition = transform.position + _targetdirection;

        _rb.MovePosition(_targetposition);

        Vector3 _roundedposition = new Vector3(Convert.ToSingle(Math.Round(transform.position.x,1)),Convert.ToSingle(Math.Round(transform.position.y,1)),Convert.ToSingle(Math.Round(transform.position.z,1)));
        Debug.Log(_roundedposition);
   
    }
    
    public static void AcumPoints(int points)
    {
        score += points;
        Debug.LogWarning(score);
    }
}