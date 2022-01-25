using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlinkyChase : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private NavMeshAgent _nav;

    private Vector3 _destinationpoint;

    public void AgressiveChase(){  
        
        _destinationpoint = _target.transform.position;
        _nav.SetDestination(_target.transform.position);
    }

    void Start(){

        _target = GameObject.Find("PacMan");
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = 4;
    }

    void Update(){
        AgressiveChase();
    }

    public void Exit()
    {

        this.enabled = false;
    }
}
