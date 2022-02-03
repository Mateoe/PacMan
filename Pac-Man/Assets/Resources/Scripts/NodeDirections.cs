using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDirections : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _directions;

    public Vector3[] GetDirections()
    {
        return _directions;
    }

}
