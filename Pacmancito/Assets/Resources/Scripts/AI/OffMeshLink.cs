using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLink : MonoBehaviour
{
    private NavMeshAgent _nav;
    private float _velocity;

    private Collider _col;



    void Start()
    {
        _nav = gameObject.GetComponent<NavMeshAgent>();
        _col = gameObject.GetComponent<Collider>();
        _velocity= _nav.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(_nav.isOnOffMeshLink)
        {
            _nav.speed = _velocity/2.5f;

        }
        else
        {
            _nav.speed = _velocity;
        }
    }
}
