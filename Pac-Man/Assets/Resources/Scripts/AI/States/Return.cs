using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Return : MonoBehaviour
{

    [SerializeField]
    private Vector3 _returnPosition;

    [SerializeField]
    private NavMeshAgent _nav;

    [SerializeField]

    private GhostID _ghostID;

    [SerializeField]

    private Animator _animator;

    [SerializeField]
    private RuntimeAnimatorController _animationNormal;

    void Start()
    {
        _nav = gameObject.GetComponent<NavMeshAgent>();
        _nav.speed = 6;
        _animator = gameObject.GetComponentInChildren<Animator>();

        if(_ghostID == GhostID.Blinlky){
            _returnPosition = new Vector3(9,0,13.5f);
            _animationNormal = Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Red_CON");

        }
        if(_ghostID == GhostID.Inky){
            _returnPosition = new Vector3(9,0,11.5f);
            _animationNormal = Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Blue_CON");

        }
        if(_ghostID == GhostID.Pinky){
            _returnPosition = new Vector3(7,0,11.5f);
            _animationNormal = Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Pink_CON");

        }
        if(_ghostID == GhostID.Clyde){
            _returnPosition = new Vector3(11,0,11.5f);
            _animationNormal = Resources.Load<UnityEngine.RuntimeAnimatorController>("Animations/Orange_CON");
        }
        EventSystem.OnGhostArrive += NormalAnim;
    }

    void Update()
    {
        _nav.SetDestination(_returnPosition);

        if(Vector3.Magnitude(gameObject.transform.position-_returnPosition)<0.05)
        {
            EventSystem.GhostArrive(_ghostID);
        }
    }

    void NormalAnim()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _animator.runtimeAnimatorController =_animationNormal;
        }
        
    }

    public void Exit()
    {

        this.enabled = false;
    }

    private void OnDestroy()
    {
        EventSystem.OnGhostArrive -= NormalAnim;
    }
}
