using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour
{

    [SerializeField]
    private Vulnerable _vulnerable;

        [SerializeField]
    private Return _return;
    private bool _active;

    [SerializeField]
    private GhostID _ghostID;
    void Start()
    {
        _vulnerable = gameObject.GetComponent<Vulnerable>();
        _return = gameObject.GetComponent<Return>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && !_vulnerable.enabled && !_return.enabled)
        {
            EventSystem.PacManDeath();
        }

        if(col.gameObject.tag == "Player" && _vulnerable.enabled)
        {
            Debug.LogError("fucking Unity");
            EventSystem.GhostDeath(_ghostID);
        }
    }
}
