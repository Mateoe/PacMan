using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private GhostID _ghostID;

    [SerializeField]

    private Animator _animator;

    [SerializeField]
    private RuntimeAnimatorController _animationNormal;

    [SerializeField]

    private Collider _col;

    void Start()
    {
        _me = this.gameObject;
        _nav = _me.GetComponent<NavMeshAgent>();
        _col = gameObject.GetComponent<Collider>();
        _nav.speed = 6;
        _animator = gameObject.GetComponentInChildren<Animator>();

        if(_ghostID == GhostID.Blinlky){
            _returnPosition = new Vector3(9,0,13.5f);
            _animationNormal = AssetDatabase.LoadAssetAtPath<UnityEngine.RuntimeAnimatorController>("Assets/Resources/Animations/Red_CON.controller");

        }
        if(_ghostID == GhostID.Inky){
            _returnPosition = new Vector3(9,0,11.5f);
            _animationNormal = AssetDatabase.LoadAssetAtPath<UnityEngine.RuntimeAnimatorController>("Assets/Resources/Animations/Blue_CON.controller");

        }
        if(_ghostID == GhostID.Pinky){
            _returnPosition = new Vector3(7,0,11.5f);
            _animationNormal = AssetDatabase.LoadAssetAtPath<UnityEngine.RuntimeAnimatorController>("Assets/Resources/Animations/Pink_CON.controller");

        }
        if(_ghostID == GhostID.Clyde){
            _returnPosition = new Vector3(11,0,11.5f);
            _animationNormal = AssetDatabase.LoadAssetAtPath<UnityEngine.RuntimeAnimatorController>("Assets/Resources/Animations/Orange_CON.controller");
        }
        EventSystem.OnGhostArrive += NormalAnim;

        _col.enabled = false;
    }

    void Update()
    {
        _nav.SetDestination(_returnPosition);

        if(Vector3.Magnitude(_me.transform.position-_returnPosition)<0.05)
        {
            _col.enabled = true;
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
}
