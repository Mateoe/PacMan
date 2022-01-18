using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Dispersion : MonoBehaviour
{
    [SerializeField]
    private GhostID _ghostID;

    [SerializeField]
    public GameObject _dispersiontarget;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private Collider _col;


    void Start()
    {
        _col = gameObject.GetComponent<Collider>();
        if(_ghostID == GhostID.Blinlky){
            _dispersiontarget = GameObject.Find("BlinkyDispersion");
        }
        if(_ghostID == GhostID.Inky){
            _dispersiontarget = GameObject.Find("InkyDispersion");
        }
        if(_ghostID == GhostID.Pinky){
            _dispersiontarget = GameObject.Find("PinkyDispersion");
        }
        if(_ghostID == GhostID.Clyde){
            _dispersiontarget = GameObject.Find("ClydeDispersion");
        }

        _nav = GetComponent<NavMeshAgent>();

        _col = gameObject.GetComponent<Collider>();
        _col.enabled = true;
        
    }

    
    void Update()
    {
        _nav.speed = 4;
        _col.enabled = true;
        SetDispersion();

        if(Vector3.Distance(transform.position,_dispersiontarget.transform.position)<0.05)
        {
            EventSystem.GhostDisperse(_ghostID);
        }
    }

    public void SetDispersion(){
        
        _nav.SetDestination(_dispersiontarget.transform.position);
    }
}
