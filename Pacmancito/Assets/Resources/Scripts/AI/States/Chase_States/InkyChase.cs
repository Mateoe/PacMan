using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InkyChase : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private GameObject _Blinky;

    [SerializeField]
    private PacManController _pacman;

    private Vector3 _destinationpoint;

    NavMeshHit _hit;
    public void PatrolChase(){
        _destinationpoint = 2*((_target.transform.position + 2*_pacman.GetDirection()) - (_Blinky.transform.position))+_Blinky.transform.position;
        
        
        if(NavMesh.SamplePosition(_destinationpoint, out _hit,10f,NavMesh.AllAreas)==true){

            _nav.SetDestination(_hit.position);
        }
        else{
            _nav.SetDestination(_destinationpoint);
        }  
    }

    void Start(){
        _target = GameObject.Find("PacMan");
        _Blinky = GameObject.Find("Blinky");
        _pacman = _target.GetComponent<PacManController>();
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = 4;
    }

    void Update(){
        PatrolChase();
    }

    public void Exit()
    {

        this.enabled = false;
    }
}
