using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClydeChase : MonoBehaviour
{
    [SerializeField]
    private GameObject _pathpointsparent;

    [SerializeField]
    private List<Transform> _pathpoints;

    [SerializeField]
    private NavMeshAgent _nav;

    private Vector3 _destinationpoint;
    
    public void RandomChase(){
        _pathpoints = new List<Transform>(_pathpointsparent.transform.childCount);

        for(int i = 0; i<_pathpointsparent.transform.childCount;i++){
            
            _pathpoints.Add(_pathpointsparent.transform.GetChild(i));
        }

        if(_nav.velocity.magnitude == 0){

            _destinationpoint = _pathpoints[Mathf.RoundToInt(Random.Range(0,62))].transform.position;
            _nav.SetDestination(_destinationpoint);
        }       
    }

    void Start(){
        _pathpointsparent = GameObject.Find("MainPoints");
        _nav = GetComponent<NavMeshAgent>();
        _nav.speed = 4;
    }

    void Update(){
        RandomChase();
    }

    public void Exit()
    {

        this.enabled = false;
    }
}
