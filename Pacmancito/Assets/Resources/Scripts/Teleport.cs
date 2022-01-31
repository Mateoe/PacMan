using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject _tel1;
    private GameObject _tel2;

    private Collider _col1;

    private Collider _col2;

    private Collider _me;
    void Start()
    {
        _tel1 = GameObject.Find("P31Teleport");
        _tel2 = GameObject.Find("P44 Teleport");

        _col1 = _tel1.GetComponent<Collider>();
        _col2 = _tel2.GetComponent<Collider>();
        _me = gameObject.GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player" && _me == _col1)
        {
            _col2.enabled = false;
            col.gameObject.transform.position = _col2.transform.position;
            Invoke("SetActive",0.5f);
        }

        if(col.gameObject.tag == "Player" && _me == _col2)
        {
            _col1.enabled = false;
            col.gameObject.transform.position = _col1.transform.position;
            Invoke("SetActive",0.5f);
        }
    }

    void SetActive()
    {
        _col1.enabled = true;
        _col2.enabled = true;
    }
}
