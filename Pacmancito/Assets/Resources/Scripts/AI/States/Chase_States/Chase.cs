using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{

    [SerializeField]
    private PacManController _pacman;

    [SerializeField]
    private GhostID _ghostID;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _Inkytarget;

    [SerializeField]
    private GameObject _pathpointsparent;

    [SerializeField]
    private List<Transform> _pathpoints;

    private Vector3 _destinationpoint;

    NavMeshHit _hit;
    

    void start(){
    }

    void Update(){

        if(_ghostID == GhostID.Blinlky){
            AgressiveChase();
        }

        if(_ghostID == GhostID.Clyde){
            RandomChase();
        }

        if(_ghostID == GhostID.Inky){
            PatrolChase();
        }

        if(_ghostID == GhostID.Pinky){
            AmbushChase();
        }
        
    }

    public void AgressiveChase(){  
        
        _destinationpoint = _target.transform.position;
        _nav.SetDestination(_target.transform.position);
    }
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

    public void PatrolChase(){
        _destinationpoint = 2*((_target.transform.position + 2*_pacman.GetDirection()) - (_Inkytarget.transform.position))+_Inkytarget.transform.position;
        
        
        if(NavMesh.SamplePosition(_destinationpoint, out _hit,10f,NavMesh.AllAreas)==true){

            _nav.SetDestination(_hit.position);
        }
        else{
            _nav.SetDestination(_destinationpoint);
        }  
    }

    public void AmbushChase(){
        _destinationpoint = (_target.transform.position + 2*_pacman.GetDirection());
        
        if(NavMesh.SamplePosition(_destinationpoint, out _hit,10f,NavMesh.AllAreas)==true){

            _nav.SetDestination(_hit.position);
        }
        else{
            _nav.SetDestination(_destinationpoint);
        }

    }
}
