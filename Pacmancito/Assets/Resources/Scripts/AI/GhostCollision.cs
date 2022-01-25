using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject _me;

    [SerializeField]
    private Vulnerable _vulnerable;
    private bool _active;

    [SerializeField]
    private GhostID _ghostID;
    void Start()
    {
        _me = this.gameObject;
        _vulnerable = _me.GetComponent<Vulnerable>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && !_vulnerable.enabled)
        {
            EventSystem.PacManDeath();
        }

        if(col.gameObject.tag == "Player" && _vulnerable.enabled)
        {
            EventSystem.GhostDeath(_ghostID);
        }
    }
}
