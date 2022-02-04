using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Vulnerable : MonoBehaviour
{
   [SerializeField]
    private GameObject _pathpointsparent;

    [SerializeField]
    private List<Transform> _pathpoints;

    [SerializeField]
    private NavMeshAgent _nav;

    private Vector3 _destinationpoint;

    [SerializeField]
    private GhostID _ghostID;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private RuntimeAnimatorController _animationVulnerable;

    [SerializeField]
    private RuntimeAnimatorController _animationNormal;

    [SerializeField]
    private RuntimeAnimatorController _animationReturn;

    private float _timer;

    private float _auxtimer;
    void Start()
    {
        _pathpointsparent = GameObject.Find("MainPoints");
        _nav = GetComponent<NavMeshAgent>();
        _timer = 8f;
        _auxtimer = _timer;
        _animator = GetComponentInChildren<Animator>();
        _animationVulnerable = Resources.Load("Animations/Vulnerable_1") as RuntimeAnimatorController;               
        _animationReturn = Resources.Load("Animations/Return") as RuntimeAnimatorController;
        if(_ghostID == GhostID.Blinlky)
        {
            _animationNormal = Resources.Load("Animations/Red_CON") as RuntimeAnimatorController;
        }
        if(_ghostID == GhostID.Inky)
        {
            _animationNormal = Resources.Load("Animations/Blue_CON") as RuntimeAnimatorController;
        }
        if(_ghostID == GhostID.Pinky)
        {
           _animationNormal = Resources.Load("Animations/Pink_CON") as RuntimeAnimatorController;
        }
        if(_ghostID == GhostID.Clyde)
        {
            _animationNormal = Resources.Load("Animations/Orange_CON") as RuntimeAnimatorController;
        }
        
        EventSystem.OnGhostDeath += ReturnAnim;
    }
    void ReturnAnim()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _animator.runtimeAnimatorController =_animationReturn;
        }
        
    }
    public void VulnerableState()
    {

        _nav.speed = 2.5f;
        _pathpoints = new List<Transform>(_pathpointsparent.transform.childCount);

        for(int i = 0; i<_pathpointsparent.transform.childCount;i++){
            
            _pathpoints.Add(_pathpointsparent.transform.GetChild(i));
        }

        if(_nav.velocity.magnitude < 0.02){

            _destinationpoint = _pathpoints[Mathf.RoundToInt(Random.Range(0,62))].transform.position;
            _nav.SetDestination(_destinationpoint);
        }     
    }

    void Update()
    {
        _timer = _timer - Time.deltaTime;

        if(_animator.runtimeAnimatorController !=_animationVulnerable)
        {
            _animator.runtimeAnimatorController =_animationVulnerable;
        }

        if(_timer < 0)
        {
            _animator.runtimeAnimatorController =_animationNormal;
            Debug.LogError("<zkmgselgnsdomhhkpt");
            EventSystem.GhostArrive(_ghostID);
        }

        Debug.Log(_timer);
        VulnerableState();
    }

    public void Exit()
    {
        _timer = _auxtimer;
        this.enabled = false;
    }

    private void OnDestroy()
    {
        EventSystem.OnGhostDeath -= ReturnAnim;
    }
}
