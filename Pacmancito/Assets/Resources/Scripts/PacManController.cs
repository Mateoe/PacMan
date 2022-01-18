using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    //Velocidad de movimiento
    public float velocity;
    [SerializeField]
    private float initvelocity;
    public GameObject _PacManBody;

    //Direccion del movimiento
    private Vector3 direction;
    Rigidbody _rb;
    public static int score = 0;

    //Definición de los nodos
    GameObject[] _Nodes;
    Vector3[] _validDirections;
    bool _inNode;
    Vector3 _newDirection;
    Sprite _PacManSprite;

    [SerializeField]
    private AudioSource _audio;


    void Start()
    {   
        //La direccion inicial es el vector de ceros
        velocity = 0;
        direction = Vector3.zero;
        _rb = GetComponent<Rigidbody>();
        _PacManBody = GameObject.Find("PacManBody");
        _audio = this.gameObject.GetComponent<AudioSource>();
        //Asignacion de nodos
        _Nodes = GameObject.FindGameObjectsWithTag("Node");
        _inNode = true;
        _newDirection = Vector3.zero;
        _PacManSprite = _PacManBody.GetComponent<SpriteRenderer>().sprite;

        EventSystem.OnPacManDeath += Death;

    }

    void Update()
    {   
        //En cada frame se lee el movimiento del jugador
        GetDirection();
        scoreRead();
        move();
        

        //Se plica el movimiento  
    }

    //se obtiene la direccion del movimiento del usuario con las teclas WASD o las flechas
    public Vector3 GetDirection()
    {
        if(Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)){
            _newDirection = Vector3.forward;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,90);

        } else if(Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)){
            _newDirection = Vector3.back;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,270);

        } else if(Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)){
            _newDirection = Vector3.left;
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,180);
             
        } else if(Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)){
            _newDirection = Vector3.right;   
            //_PacManBody.transform.rotation = Quaternion.Euler(90,0,0);
        }

        //Uso de la función inNode
        InNode();

        //Definición de la posibilidad de rotación
        if(_inNode){
            bool canRotate = false;
            bool canContinue = false;
            foreach(Vector3 validDirection in _validDirections){
                if(_newDirection == validDirection){
                    canRotate = true;
                    break;
                }else if(direction == validDirection){
                    canContinue = true;
                }
            }
            if(canRotate){
                velocity = initvelocity;
                direction = _newDirection;
                _PacManBody.GetComponent<Animator>().enabled = true;
            }else if (!canContinue){
                velocity = 0;
                _PacManBody.GetComponent<Animator>().enabled = false;
                _PacManBody.GetComponent<SpriteRenderer>().sprite = _PacManSprite;
            }
        }else if(_newDirection == direction*-1){
            direction = _newDirection;
        }

        _PacManBody.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90,0,90);
        return direction;
    }        

    void move()
    {    
        Vector3 _targetdirection = direction*velocity*Time.deltaTime;

        _rb.position += _targetdirection;
        
        if(_targetdirection.magnitude > 0 && _audio.isPlaying == false && _audio.enabled == true)
        {
            _audio.Play();
        }
        if (_targetdirection.magnitude == 0)
        {
            _audio.Stop();
        }

  
    }
    
    public static void AcumPoints(int points)
    {
        score += points;
    }

    public int scoreRead()
    {
        return score;
    }

     //Verificación si pacman está en un nodo
    public void InNode(){
        
        bool _anyNode = false;
        
        foreach(GameObject node in _Nodes){
            
            float distance =Vector3.Distance(transform.position,node.GetComponent<Transform>().position);
           
            if(Math.Abs(distance)<0.1){
                
                _validDirections = node.GetComponent<NodeDirections>().GetDirections();
                _anyNode = true;
                break;

            }
        }

        _inNode = _anyNode;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "BigPellet")
        {
            EventSystem.PacManEnergized();
        }

        if(col.gameObject.tag == "Enemy")
        {
            EventSystem.PacManDeath();
        }
    }

    void Death()
    {
        Debug.Log("You Lost");
        transform.position = new Vector3(9,0,2.5f);
    }
}