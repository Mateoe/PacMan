using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PinkyChase : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private PacManController _pacman;

    private Vector3 _destinationpoint;

    NavMeshHit _hit;

    
    public void AmbushChase(){
        _destinationpoint = (_target.transform.position + 2*_pacman.GetDirection());
        
        if(NavMesh.SamplePosition(_destinationpoint, out _hit,10f,NavMesh.AllAreas)==true){

            _nav.SetDestination(_hit.position);
        }
        else{
            _nav.SetDestination(_destinationpoint);
        }

    }
    void Start(){
        _target = GameObject.Find("PacMan");
        _pacman = _target.GetComponent<PacManController>();
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = 4;
    }

    void Update(){
        AmbushChase();
    }

    public void Exit()
    {

        this.enabled = false;
    }
}
