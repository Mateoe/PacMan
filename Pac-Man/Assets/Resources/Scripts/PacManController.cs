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
    private Animator _PacManAnimator;
    
    [SerializeField]
    private RuntimeAnimatorController _PacmanDeathAnimation;

    [SerializeField]
    private AudioSource _audio;

    private float _timedeath;
    private float _auxtimer;
    
    private bool animationDeath = false;

    private bool deathActive = false;

    private Manage _manage;

    //-- Nodo de inicio de pacman
     GameObject _startNode;
     [SerializeField]
     Sprite _startSprite;
    //-

    void Start()
    {   
        //La direccion inicial es el vector de ceros
        velocity = 0;
        direction = Vector3.zero;
        _rb = GetComponent<Rigidbody>();
        _PacManBody = GameObject.Find("PacManBody");
        _audio = this.gameObject.GetComponent<AudioSource>();
        _audio.volume = 0.2f;
        //Asignacion de nodos
        _Nodes = GameObject.FindGameObjectsWithTag("Node");
        _inNode = true;
        _newDirection = Vector3.zero;
        _PacManSprite = _PacManBody.GetComponent<SpriteRenderer>().sprite;
        
        _PacManAnimator = _PacManBody.GetComponent<Animator>();
        _PacmanDeathAnimation = Resources.Load("Animations/Pacman_Death_01") as RuntimeAnimatorController;
        _timedeath = 1.7f;
        EventSystem.OnPacManDeath += Death;

        //--Nodo de inicio de pacman
        _startNode = GameObject.Find("P66");
        _startSprite = Resources.Load<Sprite>("Sprites/PacMan/PacMan_2");
        //--

    }

    void Update()
    {   
        //En cada frame se lee el movimiento del jugador
        if (animationDeath)
        {
            _auxtimer-=Time.deltaTime;
        }
        
        else{
            GetDirection();
            scoreRead();
            move();
        }

        if (_auxtimer <= 0)
        {
            animationDeath = false;
            if (deathActive == true)
            {
                deathActive = false;
                transform.position = new Vector3(9,0,2.5f);

                //--Añadido para controlar que pacman mire hacia arriba y se quede quieto en el nodo de inicio cuando muere
                _newDirection = Vector3.forward;
                velocity = 0;
                move();
                //--

                _PacManAnimator.runtimeAnimatorController = Resources.Load("Animations/Pacman_0") as RuntimeAnimatorController;
                _PacManAnimator.enabled = false;
                EventSystem.PacManDeathExit();
            }
            

        }
        

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

        //--Calcula la distancia entre pacman y el nodo de inicio
        float _startDistance = Vector3.Distance(transform.position,_startNode.GetComponent<Transform>().position);

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

                //--Añadido para controlar la rotación en la muerte
                if (Math.Abs(_startDistance) < 0.1 && velocity == 0){
                    _PacManBody.GetComponent<SpriteRenderer>().sprite = _startSprite;
                }
                //-
                
            }
            //--Añadido para controlar la rotación en la muerte
            else if (Math.Abs(_startDistance) < 0.1 && velocity == 0){
                _PacManBody.GetComponent<SpriteRenderer>().sprite = _startSprite;
            }
            //-
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
        Manage.PelletReduce();
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
    }

    void Death()
    {
        Debug.Log("You Lost");
        _PacManAnimator.enabled = true;
        _newDirection = Vector3.forward;
        _PacManAnimator.runtimeAnimatorController = _PacmanDeathAnimation;
        _auxtimer = _timedeath;
        animationDeath = true;
        deathActive = true;
    }

    private void OnDestroy()
    {
        EventSystem.OnPacManDeath -= Death;
    }
}