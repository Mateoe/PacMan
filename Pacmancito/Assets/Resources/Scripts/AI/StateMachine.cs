using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    void Start()
    {

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

        
    }
    public void EnterChase()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _dispersionScript.enabled = false;
            _vulnerableScript.enabled = false;
            _returnScript.enabled = false;

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

        }
    }

    public void EnterVulnerable()
    {
        _dispersionScript.enabled = false;
        _vulnerableScript.enabled = true;
        _returnScript.enabled = false;

        if(_blinkyChase != null){
            _blinkyChase.enabled = false;
        }
        if(_inkyChase != null){
            _inkyChase.enabled = false;
        }
        if(_pinkyChase != null){
            _pinkyChase.enabled = false;
        }
        if(_clydeChase != null){
            _clydeChase.enabled = false;
        }     
    }

    public void EnterDispersion()
    {

        if(EventSystem.ghost == _ghostID)
        {
            _dispersionScript.enabled = true;
            _vulnerableScript.enabled = false;
            _returnScript.enabled = false;

            if(_blinkyChase != null){
                _blinkyChase.enabled = false;
            }
            if(_inkyChase != null){
                _inkyChase.enabled = false;
            }
            if(_pinkyChase != null){
                _pinkyChase.enabled = false;
            }
            if(_clydeChase != null){
                _clydeChase.enabled = false;
            }
        }
    }

    public void EnterReturn()
    {
        if(EventSystem.ghost == _ghostID)
        {
            _dispersionScript.enabled = false;
            _vulnerableScript.enabled = false;
            _returnScript.enabled = true;

            if(_blinkyChase != null){
                _blinkyChase.enabled = false;
            }
            if(_inkyChase != null){
                _inkyChase.enabled = false;
            }
            if(_pinkyChase != null){
                _pinkyChase.enabled = false;
            }
            if(_clydeChase != null){
                _clydeChase.enabled = false;
            }
        }
    }


}