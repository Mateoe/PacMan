using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    //Velocidad de movimiento
    public float velocity = 4.0f;
    public GameObject _PacManBody;

    public GameObject _CollisionDetector;
    public GameObject[] _Walls;

    public bool _isColliding;

    //Direccion del movimiento
    public Vector3 direction;
    Rigidbody _rb;
    Sprite _PacManSprite;

    void Start()
    {   
        //La direccion inicial es el vector de ceros
        direction = Vector3.zero;
        _rb = GetComponent<Rigidbody>();
        _PacManBody = GameObject.Find("PacManBody");
        _CollisionDetector = GameObject.Find("CollisionDetector");
        _Walls = GameObject.FindGameObjectsWithTag("Wall");
        _PacManSprite = _PacManBody.GetComponent<SpriteRenderer>().sprite;
        _isColliding = true;

    }

    void Update()
    {   
        //En cada frame se lee el movimiento del jugador
        GetDirection();
        //Se plica el movimiento
        IsTouchingWall();

        if(_isColliding){
            _PacManBody.GetComponent<Animator>().enabled = false;
            _PacManBody.GetComponent<SpriteRenderer>().sprite = _PacManSprite;
        }else{
           _PacManBody.GetComponent<Animator>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    //se obtiene la direccion del movimiento del usuario con las teclas WASD o las flechas
    public Vector3 GetDirection()
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
        
        return direction;
        
    }        

    void Move()
    {    
        Vector3 _targetdirection = direction*velocity*Time.deltaTime;


        Vector3 _targetposition = transform.position + _targetdirection;

        _rb.MovePosition(_targetposition);

    }

    public void IsTouchingWall(){
        bool _anyColliding = false;
        foreach (GameObject wall in _Walls)
        {
            if(_CollisionDetector.GetComponent<Collider>().bounds.Intersects(wall.GetComponent<Collider>().bounds)){
                _anyColliding = true;
            }
        }

        _isColliding = _anyColliding;

    }

}