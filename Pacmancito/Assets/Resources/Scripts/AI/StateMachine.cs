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

        EnterDispersion();
    }
    void Update()
    {
        if(Vector3.Magnitude(transform.position-_dispersionScript._dispersiontarget.transform.position)<0.05){
            EnterChase();
        }
    }

    void EnterChase()
    {
        _dispersionScript.enabled = false;
        _vulnerableScript.enabled = false;

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

    void EnterVulnerable()
    {
        _dispersionScript.enabled = false;
        _vulnerableScript.enabled = true;

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

    void EnterDispersion()
    {
        _dispersionScript.enabled = true;
        _vulnerableScript.enabled = false;

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