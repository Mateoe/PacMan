using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    private GhostID _ghostID;

    [SerializeField]
    private BlinkyChase _blinkyChase;

    [SerializeField]
    private InkyChase _inkyChase;

    [SerializeField]
    private PinkyChase _pinkyChase;

    [SerializeField]
    private ClydeChase _clydeChase;

    [SerializeField]
    private Vulnerable _vulnerableScript;

    [SerializeField]
    private Dispersion _dispersionScript;

    [SerializeField]
    private Return _returnScript;

    [SerializeField]
    private NavMeshAgent _nav;
    

    void Start()
    {
        _nav = gameObject.GetComponent<NavMeshAgent>();

        if(_ghostID == GhostID.Blinlky){
            _blinkyChase= gameObject.GetComponent<BlinkyChase>();
        }
        if(_ghostID == GhostID.Inky){
            _inkyChase= gameObject.GetComponent<InkyChase>();
        }
        if(_ghostID == GhostID.Pinky){
            _pinkyChase= gameObject.GetComponent<PinkyChase>();
        }
        if(_ghostID == GhostID.Clyde){
            _clydeChase= gameObject.GetComponent<ClydeChase>();
        }

        _vulnerableScript = gameObject.GetComponent<Vulnerable>();

        _dispersionScript = gameObject.GetComponent<Dispersion>();

        _returnScript = gameObject.GetComponent<Return>();

        EventSystem.OnGhostDisperse += EnterChase;
        EventSystem.OnPacManEnergized += EnterVulnerable;
        EventSystem.OnGhostDeath += EnterReturn;
        EventSystem.OnGhostArrive += EnterDispersion;
        EventSystem.OnGhostInit += GhostStart;
        EventSystem.GhostInit(); 
    }

    public void GhostStart()
    {

        _nav.enabled = false;
        _vulnerableScript.Exit();
        _returnScript.Exit();

        if(_blinkyChase != null){
            _blinkyChase.Exit();
        }
        if(_inkyChase != null){
            _inkyChase.Exit();
        }
        if(_pinkyChase != null){
            _pinkyChase.Exit();
        }
        if(_clydeChase != null){
            _clydeChase.Exit();
        }

        _dispersionScript.enabled = true;
        _nav.enabled = true;
    }
    public void EnterChase()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _nav.enabled = false;
            _dispersionScript.Exit();
            _vulnerableScript.Exit();
            _returnScript.Exit();

            if(_blinkyChase != null){
                _blinkyChase.enabled = true;
            }
            if(_inkyChase != null){
                _inkyChase.enabled = true;
            }
            if(_pinkyChase != null){
                _pinkyChase.enabled = true;
            }
            if(_clydeChase != null){
                _clydeChase.enabled = true;
            }
            _nav.enabled = true;

        }
    }

    public void EnterVulnerable()
    {
        _nav.enabled = false;
        _dispersionScript.Exit(); 
        _returnScript.Exit();

        if(_blinkyChase != null){
            _blinkyChase.Exit();
        }
        if(_inkyChase != null){
            _inkyChase.Exit();
        }
        if(_pinkyChase != null){
            _pinkyChase.Exit();
        }
        if(_clydeChase != null){
            _clydeChase.Exit();
        }  

        _vulnerableScript.enabled = true; 
        _nav.enabled = true;
    }

    public void EnterDispersion()
    {

        if(EventSystem.ghost == _ghostID)
        {
            _nav.enabled = false;
            _vulnerableScript.Exit();
            _returnScript.Exit();

            if(_blinkyChase != null){
                _blinkyChase.Exit();
            }
            if(_inkyChase != null){
                _inkyChase.Exit();
            }
            if(_pinkyChase != null){
                _pinkyChase.Exit();
            }
            if(_clydeChase != null){
                _clydeChase.Exit();
            }

            _dispersionScript.enabled = true;
            _nav.enabled = true;
        }
    }

    public void EnterReturn()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _nav.enabled = false;
            _dispersionScript.Exit();
            _vulnerableScript.Exit();

            if(_blinkyChase != null){
                _blinkyChase.Exit();
            }
            if(_inkyChase != null){
                _inkyChase.Exit();
            }
            if(_pinkyChase != null){
                _pinkyChase.Exit();
            }
            if(_clydeChase != null){
                _clydeChase.Exit();
            }

            _returnScript.enabled = true;
            _nav.enabled = true;
        }
    }

    private void OnDestroy()
    {
        EventSystem.OnGhostDisperse -= EnterChase;
        EventSystem.OnPacManEnergized -= EnterVulnerable;
        EventSystem.OnGhostDeath -= EnterReturn;
        EventSystem.OnGhostArrive -= EnterDispersion;
        EventSystem.OnGhostInit -= GhostStart;
    }
}