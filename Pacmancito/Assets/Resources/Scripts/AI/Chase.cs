using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    [SerializeField]
    private GhostID _ghostID;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _pathpointsparent;

    [SerializeField]
    private List<Transform> _pathpoints;

    private Vector3 _destinationpoint;
    

    void start(){
    }

    void Update(){

        if(_ghostID == GhostID.Blinlky){
            AgressiveChase();
        }

        if(_ghostID == GhostID.Clyde){
            RandomChase();
        }
        
    }

    public void AgressiveChase(){  

        _nav.SetDestination(_target.transform.position);
    }

    public void RandomChase(){
        _pathpoints = new List<Transform>(_pathpointsparent.transform.childCount);

        for(int i = 0; i<_pathpointsparent.transform.childCount;i++){
            
            _pathpoints.Add(_pathpointsparent.transform.GetChild(i));
        }

        Debug.Log(_nav.destination);

        if(_nav.velocity.magnitude == 0){

            _destinationpoint = _pathpoints[Mathf.RoundToInt(Random.Range(0,62))].transform.position;
            _nav.SetDestination(_destinationpoint);
        }

            
    }
}
