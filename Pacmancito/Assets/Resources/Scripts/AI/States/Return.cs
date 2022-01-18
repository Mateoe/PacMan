using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Return : MonoBehaviour
{
    [SerializeField]
    private GameObject _me;

    [SerializeField]
    private Vector3 _returnPosition;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]
    private Collider _col;

    [SerializeField]

    private GhostID _ghostID;

    void Start()
    {
        _me = this.gameObject;
        _nav = _me.GetComponent<NavMeshAgent>();
        _col = _me.GetComponent<Collider>();
        _nav.speed = 6;
        _col.enabled = false;

        if(_ghostID == GhostID.Blinlky){
            _returnPosition = new Vector3(9,0,13.5f);
        }
        if(_ghostID == GhostID.Inky){
            _returnPosition = new Vector3(9,0,11.5f);
        }
        if(_ghostID == GhostID.Pinky){
            _returnPosition = new Vector3(7,0,11.5f);
        }
        if(_ghostID == GhostID.Clyde){
            _returnPosition = new Vector3(11,0,11.5f);
        }
    }

    void Update()
    {
        _nav.SetDestination(_returnPosition);

        if(Vector3.Magnitude(_me.transform.position-_returnPosition)<0.05)
        {
            EventSystem.GhostArrive(_ghostID);
        }
    }
}
